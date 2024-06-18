using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeSuppliesManagement.Data;
using OfficeSuppliesManagement.Models;

namespace OfficeSuppliesManagement.Pages.Users
{
    public class InventoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public InventoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Item> Items { get; set; }

        public void OnGet()
        {
            Items = _context.Items.ToList();
        }
    }
}
