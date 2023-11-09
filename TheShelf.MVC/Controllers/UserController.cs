using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheShelf.MVC.Extension;
using TheShelf.MVC.Models;
using TheShelf.Model.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TheShelf.Service.Interface;

namespace TheShelf.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ApiService _apiService;
        private readonly IUserService _userService;

        public UserController(ApiService apiService, IUserService userService)
        {
            _apiService = apiService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var registerDto = new RegisterDto
                {
                    Username = model.Username,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password
                };

                var registrationResult = await _apiService.RegisterUser(registerDto);

                if (registrationResult.IsSuccessStatusCode)
                {
                    TempData["RegistrationSuccess"] = "Registration was successful. You can now log in.";
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var loginDto = new LoginDto
                {
                    Username = model.Username,
                    Password = model.Password
                };

                var loginResult = await _apiService.Login(loginDto);

                if (loginResult.IsSuccessStatusCode)
                {
                    var responseContent = await loginResult.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(responseContent);

                    // Redirect to the homepage (assumes "Index" is the homepage action)
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login failed. Please check your credentials.");
                }
            }
            return View(model);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var loginDto = new LoginDto
        //        {
        //            Username = model.Username,
        //            Password = model.Password
        //        };

        //        var loginResult = await _apiService.Login(loginDto);

        //        if (loginResult.IsSuccessStatusCode)
        //        {
        //            var responseContent = await loginResult.Content.ReadAsStringAsync();
        //            var loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(responseContent);

        //            return RedirectToAction("LoggedIn", new { token = loginResponse.Token });
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Login failed. Please check your credentials.");
        //        }
        //    }
        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var logoutResult = await _apiService.Logout();

                if (logoutResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("LoggedOut");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Logout failed. Please try again.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
            }

            return View("Logout");
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var resetPasswordDto = new ResetPasswordDto
                {
                    Email = model.Email,
                    Token = model.Token,
                    NewPassword = model.NewPassword
                };

                var resetResult = await _apiService.ResetPassword(resetPasswordDto);

                if (resetResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("ResetPasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to reset password. Please try again.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult GeneratePasswordResetToken()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneratePasswordResetToken(GenerateTokenDto model)
        {
            if (ModelState.IsValid)
            {
                var generateTokenDto = new GenerateTokenDto
                {
                    Email = model.Email
                };

                var tokenResult = await _apiService.GeneratePasswordResetToken(generateTokenDto);

                if (tokenResult.IsSuccessStatusCode)
                {
                    var token = await tokenResult.Content.ReadAsStringAsync();

                    ViewBag.PasswordResetToken = token;
                    return View("DisplayToken");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to generate the password reset token. Please try again.");
                }
            }
            return View(model);
        }



        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(AddUserDto model)
        {
            if (ModelState.IsValid)
            {
                var addUserDto = new AddUserDto
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };
                var response = await _apiService.CreateNewUser(addUserDto);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to create the user. Please try again.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AllUsers()
        {
            var response = await _apiService.GetAllUsers();

            if (response.IsSuccessStatusCode)
            {
                var usersJson = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<ResponseObject<IEnumerable<UserReturnDto>>>(usersJson);

                if (users != null && users.Data != null)
                {
                    return View(users.Data);
                }
            }

            return View("Error", new ErrorViewModel { StatusCode = (int)response.StatusCode, ErrorMessage = "Failed to retrieve user data." });
        }

      
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var response = await _apiService.GetUserById(id);

            if (response.IsSuccessStatusCode)
            {
                var userJson = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserReturnDto>(userJson);

                if (!string.IsNullOrEmpty(userJson))
                {
                   
                    return View(user);
                }
                else
                {
                    return View("Error", new ErrorViewModel { StatusCode = (int)response.StatusCode, ErrorMessage = "Empty response from the API." });
                }
            }
            else
            {
                return View("Error", new ErrorViewModel { StatusCode = (int)response.StatusCode, ErrorMessage = "Failed to retrieve user data." });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserReturnDto model)
        {
            if (ModelState.IsValid)
            {
                var updateUserDto = new UserReturnDto
                {
                    Id = model.Id,
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };
                var response = await _apiService.UpdateUser(updateUserDto);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update the user. Please try again.");
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var response = await _apiService.GetUserByEmail(email);

            if (response.IsSuccessStatusCode)
            {
                var userJson = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(userJson))
                {
                    var user = JsonConvert.DeserializeObject<UserReturnDto>(userJson);
                    return View(user);
                }
                else
                {
                    return View("Error", new ErrorViewModel { StatusCode = (int)response.StatusCode, ErrorMessage = "Empty response from the API." });
                }
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return View("Error", new ErrorViewModel { StatusCode = (int)response.StatusCode, ErrorMessage = "User not found." });
            }
            else
            {
                return View("Error", new ErrorViewModel { StatusCode = (int)response.StatusCode, ErrorMessage = "Failed to retrieve user data." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var response = await _apiService.GetUserByUsername(username);

            if (response.IsSuccessStatusCode)
            {
                var userJson = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(userJson))
                {
                    var user = JsonConvert.DeserializeObject<UserReturnDto>(userJson);
                    return View(user);
                }
                else
                {
                    return View("Error", new ErrorViewModel { StatusCode = (int)response.StatusCode, ErrorMessage = "Empty response from the API." });
                }
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return View("Error", new ErrorViewModel { StatusCode = (int)response.StatusCode, ErrorMessage = "User not found." });
            }
            else
            {
                return View("Error", new ErrorViewModel { StatusCode = (int)response.StatusCode, ErrorMessage = "Failed to retrieve user data." });
            }
        }

		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			try
			{
                var user = await _apiService.GetUserById(id);
				if (user == null)
				{
					return NotFound();
				}

				return View(user);
			}
			catch (Exception ex)
			{
				return BadRequest(new { ErrorMessage = ex.Message });
			}
		}



		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			try
			{
				var response = await _apiService.DeleteUser(id);

				if (response.IsSuccessStatusCode)
				{
					TempData["SuccessMessage"] = "User deleted successfully.";
				}
				else
				{
					TempData["ErrorMessage"] = "Failed to delete the user.";
				}

				return RedirectToAction("Index", "Home");
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
				return RedirectToAction("Index", "Home");
			}
		}

	}
}
