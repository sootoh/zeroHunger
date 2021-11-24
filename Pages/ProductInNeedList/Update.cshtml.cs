using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;


namespace ZeroHunger.Pages.ProductInNeedList
{
    public class UpdateModel : PageModel
    {
        private ApplicationDbContext _db;

        public UpdateModel(ApplicationDbContext db)
        {
            _db = db;

        }
        [BindProperty]
        public ProductInNeed product { get; set; }
        public async Task OnGet(int id) => product = await _db.ProductInNeed.FindAsync(id);
        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {

                var productFromDB = await _db.ProductInNeed.FindAsync(product.product_id);
                productFromDB.product_name = product.product_name;
                if (product.product_description != null)
                    productFromDB.product_description = product.product_description;

                productFromDB.amount = product.amount;

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");

            }
            return RedirectToPage();
        }
    }
}
