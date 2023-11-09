using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheShelf.Model.Dtos;
using TheShelf.Model.Entities;
using TheShelf.Service.Interface;

namespace TheShelf.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserReturnDto>> GetUsersAsync()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                return _mapper.Map<IEnumerable<UserReturnDto>>(users);
            }
            catch (Exception ex)
            {
                return new List<UserReturnDto> { new UserReturnDto { Errors = new List<string> { ex.Message } } };
            }
        }

        public async Task<UserReturnDto> GetUserByIdAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                return _mapper.Map<UserReturnDto>(user);
            }
            catch (Exception ex)
            {
                return new UserReturnDto { Errors = new List<string> { ex.Message } };
            }
        }

        public async Task<UserReturnDto> CreateUserAsync(AddUserDto addUserDto)
        {
            try
            {
                var user = _mapper.Map<User>(addUserDto);
                var result = await _userManager.CreateAsync(user, addUserDto.Password);

                if (result.Succeeded)
                {
                    return _mapper.Map<UserReturnDto>(user);
                }
                else
                {
                    return new UserReturnDto { ErrorMessage = "User creation failed. Please check the provided information." };
                }
            }
            catch (Exception ex)
            {
                return new UserReturnDto { ErrorMessage = ex.Message };
            }
        }

        public async Task<UserReturnDto> UpdateUserAsync(string id, UpdateUserDto updateUserDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user != null)
                {
                    _mapper.Map(updateUserDto, user);
                    var updateResult = await _userManager.UpdateAsync(user);

                    if (updateResult.Succeeded)
                    {
                        return _mapper.Map<UserReturnDto>(user);
                    }
                    else
                    {
                        return new UserReturnDto { ErrorMessage = "User update failed." };
                    }
                }
                else
                {
                    return new UserReturnDto { ErrorMessage = "User not found." };
                }
            }
            catch (Exception ex)
            {
                return new UserReturnDto { ErrorMessage = ex.Message };
            }
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    return result.Succeeded;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _userManager.FindByEmailAsync(email);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            try
            {
                return await _userManager.FindByNameAsync(username);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
