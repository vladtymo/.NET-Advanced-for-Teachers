using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace AspNetMvcApplication.Helpers
{
    public enum Roles
    {
        Admin, 
        User
    }

    public static class Seeder
    {
        public static async Task SeedRoles(IServiceProvider provider)
        {
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in Enum.GetNames(typeof(Roles)))
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedAdminUser(IServiceProvider provider)
        {
            var userManager = provider.GetRequiredService<UserManager<User>>();

            const string USERNAME = "myadmin@myadmin.com";
            const string PASSWORD = "Admin1@";

            var existingUser = await userManager.FindByNameAsync(USERNAME);

            if (existingUser == null)
            {
                var user = new User
                {
                    UserName = USERNAME,
                    Email = USERNAME,
                };

                var result = await userManager.CreateAsync(user, PASSWORD);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

        }
    }
}
