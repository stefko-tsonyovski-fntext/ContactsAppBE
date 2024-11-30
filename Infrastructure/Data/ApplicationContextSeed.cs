using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace Infrastructure.Data
{
    public class ApplicationContextSeed
    {
        public static async Task SeedAsync(ApplicationContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await context.Users.AnyAsync())
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    FullName = "Bob Marley"
                };

                string password = "Pa$$w0rd";

                await userManager.CreateAsync(user, password);
            }
        }
    }
}
