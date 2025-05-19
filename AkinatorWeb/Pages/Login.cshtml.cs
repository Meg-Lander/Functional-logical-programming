using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AkinatorWeb.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public LoginModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public string Username { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                Message = "Имя не может быть пустым!";
                return Page();
            }

            var client = _clientFactory.CreateClient();
            var users = await client.GetFromJsonAsync<List<User>>("https://localhost:7025/api/users");

            var user = users.Find(u => u.Username == Username);

            if (user == null)
            {
                var response = await client.PostAsync("https://localhost:7025/api/users/register",
                    new StringContent(JsonSerializer.Serialize(new User { Username = Username, PasswordHash = "none" }),
                    Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<User>();
                    if (created != null)
                    {
                        user = created;
                    }
                    else
                    {
                        Message = "Ошибка при регистрации пользователя.";
                        return Page();
                    }
                }
                else
                {
                    Message = "Ошибка запроса к API при регистрации.";
                    return Page();
                }

            }

            if (user != null && !string.IsNullOrWhiteSpace(user.Username))
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("Username", user.Username); 
                return RedirectToPage("/Home");

            }
            else
            {
                Message = "Не удалось получить пользователя.";
                return Page();
            }
        }

        public class User
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string PasswordHash { get; set; }
        }
    }
}
