using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TheShelf.MVC.Models;
using TheShelf.Model.Dtos;
using System.Text;

namespace TheShelf.MVC.Extension
{
    public class ApiService
    {
        private readonly string _apiBaseUrl;
        private readonly HttpClient _httpClient;

        public ApiService(IOptions<ApiSettings> apiSettings)
        {
            _apiBaseUrl = apiSettings.Value.BaseUrl;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_apiBaseUrl)
            };
        }

        public async Task<HttpResponseMessage> RegisterUser(RegisterDto model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("api/user/register", content);
        }

        public async Task<HttpResponseMessage> Login(LoginDto model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("api/user/login", content);
        }

        public async Task<HttpResponseMessage> Logout()
        {
            return await _httpClient.PostAsync("api/user/logout", null);
        }

        public async Task<HttpResponseMessage> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(resetPasswordDto), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("api/user/reset-password", content);
        }

        public async Task<HttpResponseMessage> GeneratePasswordResetToken(GenerateTokenDto generateTokenViewModel)
        {
            var content = new StringContent(JsonConvert.SerializeObject(generateTokenViewModel), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("api/user/generate-password-reset-token", content);
        }


        public async Task<HttpResponseMessage> CreateNewUser(AddUserDto model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("api/user/add-user", content);
        }


        public async Task<HttpResponseMessage> GetAllUsers()
        {
            string apiUrl = "api/user/get-all-users";

            return await _httpClient.GetAsync(apiUrl);
        }

        public async Task<HttpResponseMessage> GetUserById(string userId)
        {
            string apiUrl = string.Format("api/user/id?id={0}", userId);

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            return response;
        }

        public async Task<HttpResponseMessage> GetUserByEmail(string email)
        {
            string apiUrl = $"api/user/email?email={email}";
            return await _httpClient.GetAsync(apiUrl);
        }

        public async Task<HttpResponseMessage> GetUserByUsername(string username)
        {
            string apiUrl = $"api/user/username?username={username}";
            return await _httpClient.GetAsync(apiUrl);
        }

        public async Task<HttpResponseMessage> UpdateUser(UserReturnDto model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            return await _httpClient.PutAsync($"api/user/update/{model.Id}", content);
        }

        public async Task<HttpResponseMessage> DeleteUser(string userId)
        {
            string apiUrl = $"api/user/delete/{userId}";

            HttpResponseMessage response = await _httpClient.DeleteAsync(apiUrl);

            return response;
        }

        public async Task<HttpResponseMessage> CreateBookAsync(CreateBookDto bookDto)
        {
            var apiUrl = "api/book/add-book";

            var content = new StringContent(JsonConvert.SerializeObject(bookDto), Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(apiUrl, content);
        }

        public async Task<HttpResponseMessage> GetBookById(string id)
        {
            string apiUrl = string.Format("api/book/id?id={0}", id); // Adjust the API route as needed

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            return response;
        }

        public async Task<HttpResponseMessage> GetAllBooks()
        {
            string apiUrl = "api/book/list-of-books";
            return await _httpClient.GetAsync(apiUrl);
        }

        public async Task<HttpResponseMessage> UpdateBook(string id, UpdateBookDto model)
        {
            string apiUrl = $"api/book/update/{id}";
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            return await _httpClient.PutAsync(apiUrl, content);
        }

        public async Task<HttpResponseMessage> DeleteBook(string id)
        {
            string apiUrl = $"api/book/delete/{id}";
            return await _httpClient.DeleteAsync(apiUrl);
        }

        public async Task<HttpResponseMessage> SearchBooks(string searchCriteria)
        {
            var apiUrl = $"api/book/search?searchCriteria={searchCriteria}";
            return await _httpClient.GetAsync(apiUrl);
        }


        public async Task<HttpResponseMessage> UploadBookImage(string id, IFormFile photo)
        {
            var apiUrl = $"api/book/photo/{id}";

            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StreamContent(photo.OpenReadStream()), "photo", photo.FileName);

                return await _httpClient.PatchAsync(apiUrl, content);
            }
        }
    }
}
