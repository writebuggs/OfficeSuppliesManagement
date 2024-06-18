using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeSuppliesManagement.Data;
using OfficeSuppliesManagement.Models;

namespace OfficeSuppliesManagement.Pages.Users
{
    public class RequestItemModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RequestItemModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Outbound NewOutbound { get; set; }

        public List<Item> Items { get; set; }
        public List<Outbound> Outbounds { get; set; }

        public void OnGet()
        {
            LoadData();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                NewOutbound.UserId = User.Identity.Name;  // 或其他用户标识
                NewOutbound.OutboundDate = DateTime.Now;
                NewOutbound.Status = "Requested";
                _context.Outbounds.Add(NewOutbound);
                await _context.SaveChangesAsync();
                return RedirectToPage();
            }
            LoadData();
            return Page();
        }

        private void LoadData()
        {
            Items = _context.Items.ToList();
            Outbounds = _context.Outbounds.Where(o => o.UserId == User.Identity.Name).ToList();
        }
    }
}
