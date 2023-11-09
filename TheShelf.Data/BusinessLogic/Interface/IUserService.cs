using TheShelf.Model.Dtos;
using TheShelf.Model.Entities;

namespace TheShelf.Service.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserReturnDto>> GetUsersAsync();
        Task<UserReturnDto> GetUserByIdAsync(string id);
        Task<UserReturnDto> CreateUserAsync(AddUserDto addUserDto);
        Task<UserReturnDto> UpdateUserAsync(string id, UpdateUserDto updateUserDto);
        Task<bool> DeleteUserAsync(string id);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
