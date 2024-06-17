using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OfficeSuppliesManagement.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }

        public IActionResult OnPost()
        {
            if (UserName == "admin" && Password == "0000")
            {
                return RedirectToPage("/Admin/Index");
            }
            ErrorMessage = "Invalid username or password";
            return Page();
        }
    }
}
