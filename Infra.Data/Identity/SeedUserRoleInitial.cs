using Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private static readonly string DefaultMail = "usuario@localhost";
        private static readonly string DefaultRole = "User";
        private static readonly string AdminMail = "admin@localhost";
        private static readonly string AdminRole = "Admin";

        public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRole()
        {
            if (!_roleManager.RoleExistsAsync(DefaultRole).Result)
            {
                IdentityRole role = new IdentityRole { Name = DefaultRole, NormalizedName = DefaultRole.ToUpper() };

                IdentityResult result = _roleManager.CreateAsync(role).Result;
            }

            if (!_roleManager.RoleExistsAsync(AdminRole).Result)
            {
                IdentityRole role = new IdentityRole { Name = AdminRole, NormalizedName = AdminRole.ToUpper() };

                IdentityResult result = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUser()
        {

            if (_userManager.FindByEmailAsync(DefaultMail).Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = DefaultMail,
                    Email = DefaultMail,
                    NormalizedUserName = DefaultMail.ToUpper(),
                    NormalizedEmail = DefaultMail.ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                IdentityResult result = _userManager.CreateAsync(user, "SenhaForte123%%").Result;

                if(result.Succeeded)
                    _userManager.AddToRoleAsync(user, DefaultRole).Wait();

            }

            if (_userManager.FindByEmailAsync(AdminMail).Result == null)
            {
                ApplicationUser adminUser = new ApplicationUser()
                {
                    UserName = AdminMail,
                    Email = AdminMail,
                    NormalizedUserName = AdminMail.ToUpper(),
                    NormalizedEmail = AdminMail.ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                IdentityResult result = _userManager.CreateAsync(adminUser, "SenhaForte123%%").Result;

                if (result.Succeeded)
                    _userManager.AddToRoleAsync(adminUser, AdminRole).Wait();
            }
        }
    }
}
