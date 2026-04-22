USE AYMDatingDB;
GO

create PROCEDURE dbo.UpdateUserPackages
AS
BEGIN
    DECLARE @UserId UNIQUEIDENTIFIER;
    DECLARE @NewPackageId INT = 1;

    -- Cursor to iterate through all valid users
    DECLARE user_cursor CURSOR FOR
    SELECT Id
    FROM AYMDatingDB.ASecurity.Users
    WHERE IsStopped = 0
      AND IsDeleted = 0
      AND Activated = 1;
    
    -- Open cursor
    OPEN user_cursor;
    
    -- Fetch the first user
    FETCH NEXT FROM user_cursor INTO @UserId;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN        
        -- Check if the user is in role 'User'
        IF EXISTS (
            SELECT 1
            FROM AYMDatingDB.ASecurity.UserRoles AS a
            JOIN AYMDatingDB.ASecurity.Roles AS b ON b.Id = a.RoleId
            WHERE a.UserId = @UserId
              AND b.Name = 'User'
        )
        BEGIN            
            -- Check for expired packages
            IF EXISTS (
                SELECT 1
                FROM AYMDatingDB.BDataSchema.UserPackages
                WHERE AppUserId = @UserId
                  AND PackageId != @NewPackageId
                  AND IsDeleted = 0
                  AND GETDATE() > PackageEndDate
            )
            BEGIN
                
                -- Mark the old package as deleted
                UPDATE AYMDatingDB.BDataSchema.UserPackages
                SET IsDeleted = 1
                WHERE AppUserId = @UserId
                  AND PackageId != @NewPackageId
                  AND IsDeleted = 0
                  AND GETDATE() > PackageEndDate;

                -- Insert a new row with the updated package
                INSERT INTO AYMDatingDB.BDataSchema.UserPackages (AppUserId, PackageId, PackageEndDate, IsDeleted, CreatedUserID, CreationDate, LastUpdateUserID,LastUpdateDate)
                SELECT AppUserId, @NewPackageId, PackageEndDate, 0, CreatedUserID, GETDATE(),Null,GETDATE()
                FROM AYMDatingDB.BDataSchema.UserPackages
                WHERE AppUserId = @UserId
                  AND PackageId != @NewPackageId
                  AND IsDeleted = 1
                  AND GETDATE() > PackageEndDate;


				 UPDATE AYMDatingDB.BDataSchema.UserHistories
				SET 
					UserPackageId = (
						SELECT TOP 1 ID 
						FROM AYMDatingDB.BDataSchema.UserPackages
						WHERE AppUserId = @UserId AND IsDeleted = 0 
						ORDER BY CreationDate DESC
					)
				WHERE 
					AppUserId = @UserId and IsMain = 1 and IsSwitchedOff = 0 AND IsDeleted = 0;

            END
        END
        
        -- Fetch the next user
        FETCH NEXT FROM user_cursor INTO @UserId;
    END
    
    -- Close and deallocate the cursor
    CLOSE user_cursor;
    DEALLOCATE user_cursor;
END;

	EXEC UpdateUserPackages;
