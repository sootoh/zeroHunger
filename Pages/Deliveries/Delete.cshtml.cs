using ZeroHunger.Data;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZeroHunger.Pages.Deliveries
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Delivery Delivery { set; get; }
        public SelectList VolunteerList { get; set; }
        public SelectList ReceiverList { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task OnGet(int id)
        {
            /*var items = await _db.User.Where(u => u.UserType.TypeID == 2).ToListAsync();
            VolunteerList = new SelectList(items, "UserID", "UserName");
            var bitems = await _db.User.Where(u => u.UserType.TypeID == 3).ToListAsync(); ;
            ReceiverList = new SelectList(bitems, "UserID", "UserName");*/

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
