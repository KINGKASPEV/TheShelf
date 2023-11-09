using Microsoft.AspNetCore.Identity;
using TheShelf.Model.Entities;
using TheShelf.Service.Interface;

namespace TheShelf.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(User user, string password)
        {
            // Implement user registration logic using _userManager
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<SignInResult> LoginAsync(string username, string password)
        {
            // Implement user login logic using _signInManager
            return await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
        }

        public async Task SignOutAsync()
        {
            // Implement user sign out logic using _signInManager
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        {
            // Implement password reset logic using _userManager
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            // Generate a password reset token for the user
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            // Find a user by their email
            return await _userManager.FindByEmailAsync(email);
        }
    }
}



