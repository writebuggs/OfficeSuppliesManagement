using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeSuppliesManagement.Data;
using OfficeSuppliesManagement.Models;

namespace OfficeSuppliesManagement.Pages.Admin
{
    public class InventoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public InventoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Item> Items { get; set; }
        public List<Inbound> Inbounds { get; set; }
        public List<Outbound> Outbounds { get; set; }

        public void OnGet()
        {
            Items = _context.Items.ToList();
            Inbounds = _context.Inbounds.ToList();
            Outbounds = _context.Outbounds.ToList();
        }
    }
}
