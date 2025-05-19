using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SQLite;
using System.Runtime.InteropServices;

namespace AkinatorWeb.Pages
{
    public class HomeModel : PageModel
    {
        public string Username { get; set; }
        public List<string> PreviousCharacters { get; set; } = new();

        public IActionResult OnGet()
        {
            Username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(Username))
                return RedirectToPage("/Login");

            using var conn = new SQLiteConnection("Data Source=C:\\Users\\admin\\OneDrive\\Рабочий стол\\прога\\AkinatorApi\\AkinatorApi\\akinator.db");
            conn.Open();

            using var cmd = new SQLiteCommand(@"
                SELECT s.CharacterName
                FROM Sessions s
                JOIN Users u ON s.UserId = u.Id
                WHERE u.Username = @user
                ORDER BY s.StartedAt DESC
            ", conn);

            cmd.Parameters.AddWithValue("@user", Username);


            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                PreviousCharacters.Add(reader.GetString(0));
            }

            return Page();
        }

    }
}
