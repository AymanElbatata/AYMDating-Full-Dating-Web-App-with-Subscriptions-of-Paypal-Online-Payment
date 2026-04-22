USE msdb;
GO

-- Step 1: Create the job
EXEC sp_add_job 
    @job_name = N'UpdateUserPackagesJob';

-- Step 2: Add a step to the job to execute the stored procedure
EXEC sp_add_jobstep 
    @job_name = N'UpdateUserPackagesJob',
    @step_name = N'ExecuteUpdateUserPackages',
    @subsystem = N'TSQL',
    @command = N'EXEC AYMDatingDB.dbo.UpdateUserPackages;', 
    @retry_attempts = 5,
    @retry_interval = 5;

-- Step 3: Create a schedule that runs every 12 hours
EXEC sp_add_schedule 
    @schedule_name = N'Every12HoursSchedule',
    @freq_type = 4,  -- Daily
    @freq_interval = 1,  -- Every day
    @freq_subday_type = 8,  -- Hours
    @freq_subday_interval = 12,  -- Every 12 hours
    @active_start_time = 000000;  -- Starts at midnight (00:00:00)

-- Step 4: Attach the schedule to the job
EXEC sp_attach_schedule 
    @job_name = N'UpdateUserPackagesJob',
    @schedule_name = N'Every12HoursSchedule';

-- Step 5: Enable the job
EXEC sp_update_job 
    @job_name = N'UpdateUserPackagesJob',
    @enabled = 1;

GO