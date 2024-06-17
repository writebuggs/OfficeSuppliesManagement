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
    public class InboundItemsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public InboundItemsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Inbound NewInbound { get; set; }

        public List<Inbound> Inbounds { get; set; }

        public void OnGet()
        {
            Inbounds = _context.Inbounds.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                NewInbound.TotalPrice = NewInbound.PurchaseQuantity * NewInbound.UnitPrice;
                _context.Inbounds.Add(NewInbound);

                var item = await _context.Items.FindAsync(NewInbound.ItemCode);
                if (item != null)
                {
                    item.Quantity += NewInbound.PurchaseQuantity;
                    _context.Items.Update(item);
                }

                await _context.SaveChangesAsync();
                return RedirectToPage();
            }
            return Page();
        }
    }
}
