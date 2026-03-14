using Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticateService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var result = _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false).Result;

            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            ApplicationUser applicationUser = new ApplicationUser { UserName = email, Email = email };

            var result = _userManager.CreateAsync(applicationUser, password).Result;
             
            if(result.Succeeded)
                await _signInManager.SignInAsync(applicationUser, isPersistent: false);

            return result.Succeeded;
        }
    }
}
