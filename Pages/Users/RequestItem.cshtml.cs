using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            var userName = HttpContext.Session.GetString("UserName");

            if ( !string.IsNullOrEmpty(userName))
            {
                NewOutbound.UserId = userName;  // 从会话中获取当前用户标识
                NewOutbound.OutboundDate = DateTime.Now;
                NewOutbound.Status = "Requested";

                _context.Outbounds.Add(NewOutbound);
                await _context.SaveChangesAsync();

                return RedirectToPage();  // 重新加载页面
            }
            else
            {
                // 输出ModelState错误信息到控制台
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var value = ModelState[modelStateKey];
                    var errors = value.Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Error in {modelStateKey}: {error.ErrorMessage}");
                    }
                }
            }

            LoadData();  // 如果ModelState无效，重新加载数据
            return Page();
        }

        private void LoadData()
        {
            Items = _context.Items.ToList();
            var userName = HttpContext.Session.GetString("UserName");
            Outbounds = _context.Outbounds.Where(o => o.UserId == userName).ToList();
        }
    }
}
