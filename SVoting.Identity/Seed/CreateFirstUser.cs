using Microsoft.AspNetCore.Identity;
using SVoting.Identity.Models;

namespace SVoting.Identity.Seed
{
    public static class CreateFirstUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var applicationUser = new ApplicationUser
            {
                FirstName = "Master",
                LastName = "User",
                UserName = "MasterUser",
                Email = "masteruser@email.com",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(applicationUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(applicationUser, "MasterPassword&01?");
            }
        }
    }
}
