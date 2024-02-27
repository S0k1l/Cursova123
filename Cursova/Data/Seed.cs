using Cursova.Models;
using Microsoft.AspNetCore.Identity;

namespace Cursova.Data
{
    public class Seed
    {
        public static async Task SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string UserMail = "mymail@email.com";

                var adminUser = await userManager.FindByEmailAsync(UserMail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "Yurii",
                        Email = "mymail@email.com",
                        PhoneNumber = "123456789",
                        FirstName = "Юрій",
                        LastName = "Мартинюк",
                        MiddleName = "Павлович",
                        Buildind = "2",
                        Street = "Львівська",
                        Apartment = "29",
                    };
                    await userManager.CreateAsync(newAdminUser, "A1mymail@email.com");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }
    }
}
