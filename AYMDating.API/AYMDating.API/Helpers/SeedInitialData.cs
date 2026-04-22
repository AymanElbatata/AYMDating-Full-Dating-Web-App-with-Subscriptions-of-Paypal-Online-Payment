using AYMDating.DAL.BaseEntity;
using AYMDating.DAL.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AYMDating.API.Helpers
{
    public class SeedInitialData
    {
        public static async void SeedData(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = services.GetRequiredService<AYMDatingContext>();
                    await context.Database.MigrateAsync();

                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

                    await AYMDatingContextSeed.SeedAsync(context, userManager, roleManager, loggerFactory);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex.Message);
                }

            }
        }
    }
}
