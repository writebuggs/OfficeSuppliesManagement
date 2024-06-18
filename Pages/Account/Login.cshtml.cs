using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeSuppliesManagement.Models;

namespace OfficeSuppliesManagement.Pages.Account
{
    public class LoginModel : PageModel
    {
        private const string FilePath = "Data/users.json";

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnPost()
        {
            if (UserName == "admin" && Password == "0000")
            {
                HttpContext.Session.SetString("UserName", "admin");
                return RedirectToPage("/Admin/Index");
            }

            var users = LoadUsers();
            var user = users.SingleOrDefault(u => u.UserId == UserName && u.Password == Password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserName", user.UserId);
                return RedirectToPage("/Users/Index");
            }

            ErrorMessage = "Invalid username or password";
            return Page();
        }

        private List<User> LoadUsers()
        {
            if (System.IO.File.Exists(FilePath))
            {
                var json = System.IO.File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<User>>(json);
            }
            return new List<User>();
        }
    }
}
