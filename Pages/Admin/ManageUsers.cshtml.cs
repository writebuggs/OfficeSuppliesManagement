using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeSuppliesManagement.Models;

namespace OfficeSuppliesManagement.Pages.Admin
{
    public class ManageUsersModel : PageModel
    {
        private const string FilePath = "Data/users.json";
        public List<User> Users { get; set; }

        [BindProperty]
        public User NewUser { get; set; }

        public void OnGet()
        {
            LoadUsers();
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                Users.Add(NewUser);
                SaveUsers();
                ModelState.Clear();
            }
        }

        public IActionResult OnPostDelete(string id)
        {
            var user = Users.Find(u => u.UserId == id);
            if (user != null)
            {
                Users.Remove(user);
                SaveUsers();
            }
            return RedirectToPage();
        }

        private void LoadUsers()
        {
            if (System.IO.File.Exists(FilePath))
            {
                var json = System.IO.File.ReadAllText(FilePath);
                Users = JsonSerializer.Deserialize<List<User>>(json);
            }
            else
            {
                Users = new List<User>();
            }
        }

        private void SaveUsers()
        {
            var json = JsonSerializer.Serialize(Users);
            System.IO.File.WriteAllText(FilePath, json);
        }
    }
}
