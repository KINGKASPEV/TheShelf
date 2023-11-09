using Microsoft.AspNetCore.Identity;
using TheShelf.Model.Entities;

namespace TheShelf.Service.Interface
{
    public interface IAuthService
    {

        Task<IdentityResult> RegisterAsync(User user, string password);
        Task<SignInResult> LoginAsync(string username, string password);
        Task SignOutAsync();
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
        Task<string> GeneratePasswordResetTokenAsync(User user);
        Task<User> FindByEmailAsync(string email);
    }
}
