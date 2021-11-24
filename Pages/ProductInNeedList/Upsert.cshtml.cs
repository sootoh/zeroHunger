using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;
using zeroHunger.Model;
using ZeroHunger.Data;

namespace zeroHunger.Pages.ProductInNeedList
{
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public UpsertModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }
        [BindProperty]
        public ProductInNeed product { get; set; }
        
        
        public async Task<IActionResult> OnGet(int? id)
        {
            product = new ProductInNeed();
            if (id == null)
            {
                return Page();
            }
            product = await _db.ProductInNeed.FirstOrDefaultAsync(u => u.product_id == id);
            if(product == null)
            {
                return NotFound();
            }
            return Page();
        }

        
        
        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {
               

                if (product.product_id == 0)
                {
                    
                    _db.ProductInNeed.Add(product); 
                }
                else
                {
                    
                    _db.ProductInNeed.Update(product);
                }

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");

            }
            return RedirectToPage();
        }
    }
}
