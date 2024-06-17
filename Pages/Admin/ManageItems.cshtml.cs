// ManageItems.cshtml.cs
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeSuppliesManagement.Data;
using OfficeSuppliesManagement.Models;

namespace OfficeSuppliesManagement.Pages.Admin
{
    public class ManageItemsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ManageItemsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Item NewItem { get; set; }
        [BindProperty]
        public IFormFile ImagePath { get; set; }

        public List<Item> Items { get; set; }

        public void OnGet()
        {
            Items = _context.Items.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (ImagePath != null && ImagePath.Length > 0)
                {
                    var fileName = Path.GetFileName(ImagePath.FileName);
                    var filePath = Path.Combine("wwwroot/images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImagePath.CopyToAsync(fileStream);
                    }
                    NewItem.ImagePath = "/images/" + fileName;
                }
                _context.Items.Add(NewItem);
                await _context.SaveChangesAsync();
                return RedirectToPage();
            }

            foreach (var modelStateKey in ModelState.Keys)
            {
                var value = ModelState[modelStateKey];
                var errors = value.Errors;
                foreach (var error in errors)
                {
                    Console.WriteLine($"Error in {modelStateKey}: {error.ErrorMessage}");
                }
            }

            return Page();
        }



        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
