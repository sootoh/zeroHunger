using ZeroHunger.Data;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZeroHunger.Pages.Deliveries
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Delivery Delivery { set; get; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            /*if(Delivery.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "The DisplayOrder cannot exactly match the Name.");
            }*/
            if (ModelState.IsValid)
            {
                await _db.Delivery.AddAsync(Delivery);
                await _db.SaveChangesAsync();
                TempData["success"] = "Delivery request created successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
