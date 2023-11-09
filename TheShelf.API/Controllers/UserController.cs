using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheShelf.API.Services.Interface;
using TheShelf.Model.Dtos;
using TheShelf.Model.Entities;
using TheShelf.Service.Interface;

namespace TheShelf.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authenticationService;
        private readonly IMapper _mapper;
        private readonly IJwt _jwt;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(
            IUserService userService,
            IAuthService authenticationService,
            IMapper mapper, IJwt jwt, UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _mapper = mapper;
            _jwt = jwt;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            try
            {
                var user = _mapper.Map<User>(registerDto);
                var result = await _authenticationService.RegisterAsync(user, registerDto.Password);

                if (result.Succeeded)
                {
                    var role = await _userManager.AddToRoleAsync(user, registerDto.Password);
                    if (role.Succeeded)
                    {
                        return Ok("Registration successful");
                    }
                   
                }

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = await _authenticationService.LoginAsync(loginDto.Username, loginDto.Password);

                if (result.Succeeded)
                {
                    var token = _jwt.CreateToken(loginDto);
                    return Ok(new { Message = "Login successful", Token = token });
                }

                return Unauthorized("Login failed");
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }


        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userService.GetUsersAsync();
                return Ok(new ResponseObject<IEnumerable<UserReturnDto>>
                {
                    Data = users
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(new ResponseObject<UserReturnDto>
                {
                    Data = user
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("email")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            try
            {
                var user = await _userService.GetUserByEmailAsync(email);

                if (user != null)
                {
                    return Ok(_mapper.Map<UserReturnDto>(user));
                }

                return NotFound("User not found");
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("username")]
        public async Task<IActionResult> GetUserByUsername([FromQuery] string username)
        {
            try
            {
                var user = await _userService.GetUserByUsernameAsync(username);

                if (user != null)
                {
                    return Ok(_mapper.Map<UserReturnDto>(user));
                }

                return NotFound("User not found");
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }


        [HttpPost("Add-User")]
        public async Task<IActionResult> CreateUser([FromBody] AddUserDto addUserDto)
        {
            try
            {
                var user = await _userService.CreateUserAsync(addUserDto);
                if (user.ErrorMessage != null)
                {
                    return BadRequest(new { ErrorMessage = user.ErrorMessage });
                }

                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, new ResponseObject<UserReturnDto>
                {
                    Data = user
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto updateUserDto)
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(id, updateUserDto);
                if (updatedUser == null || updatedUser.ErrorMessage != null)
                {
                    return NotFound(new { ErrorMessage = updatedUser?.ErrorMessage ?? "User not found." });
                }

                return Ok(new ResponseObject<UserReturnDto>
                {
                    Data = updatedUser
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                if (!result)
                {
                    return NotFound(new { ErrorMessage = "User not found or deletion failed." });
                }
                return Ok(new { Message = "User deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }



        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            try
            {
                await _authenticationService.SignOutAsync();
                return Ok("Logout successful");
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto resetPasswordDto)
        {
            try
            {
                var user = await _authenticationService.FindByEmailAsync(resetPasswordDto.Email);
                if (user == null)
                {
                    return BadRequest("User not found");
                }

                var result = await _authenticationService.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
                if (result.Succeeded)
                {
                    return Ok("Password reset successful");
                }
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        [HttpPost("generate-password-reset-token")]
        public async Task<IActionResult> GeneratePasswordResetTokenAsync([FromBody] GenerateTokenDto generateTokenDto)
        {
            try
            {
                var user = await _authenticationService.FindByEmailAsync(generateTokenDto.Email);
                if (user == null)
                {
                    return BadRequest("User not found");
                }

                var token = await _authenticationService.GeneratePasswordResetTokenAsync(user);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }
    }
}
