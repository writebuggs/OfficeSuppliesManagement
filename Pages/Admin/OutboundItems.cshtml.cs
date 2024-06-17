using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeSuppliesManagement.Data;
using OfficeSuppliesManagement.Models;

namespace OfficeSuppliesManagement.Pages.Admin
{
    public class OutboundItemsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public OutboundItemsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Outbound NewOutbound { get; set; }

        public List<Outbound> Outbounds { get; set; }

        public void OnGet()
        {
            Outbounds = _context.Outbounds.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                _context.Outbounds.Add(NewOutbound);

                if (NewOutbound.Status == "Confirmed")
                {
                    var item = await _context.Items.FindAsync(NewOutbound.ItemCode);
                    if (item != null)
                    {
                        item.Quantity -= NewOutbound.Quantity;
                        _context.Items.Update(item);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToPage();
            }
            return Page();
        }
    }
}
