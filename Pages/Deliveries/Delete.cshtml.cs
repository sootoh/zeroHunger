using ZeroHunger.Data;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ZeroHunger.Pages.Deliveries
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Delivery Delivery { set; get; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            Delivery = _db.Delivery.Find(id);
        }
        public async Task<IActionResult> OnPost()
        {
            var deliveryFromDb = _db.Delivery.Find(Delivery.DeliveryID);
            if (deliveryFromDb != null)
            {
                _db.Delivery.Remove(deliveryFromDb);
                await _db.SaveChangesAsync();
                TempData["success"] = "Delivery request deleted successfully";
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
