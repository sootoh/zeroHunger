using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.Deliveries
{
    public class EditDeliveryItemModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public int DeliveryId { get; set; }
        public int Quantity { get; set; }
        [BindProperty]
        public DeliveryItem DeliveryItem { set; get; }
        public Delivery Delivery { set; get; }
        public SelectList DeliveryItemList { set; get; }

        public EditDeliveryItemModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task OnGet(int id)
        {
            DeliveryId = id;
            Delivery = _db.Delivery.Find(id);
            DeliveryItem = (DeliveryItem)_db.DeliveryItem.Where(d => d.DeliveryID == id);
            Quantity = DeliveryItem.Quantity;
            var deliveryitems = await _db.DryFoodDonation.Where(d => d.DryFoodRemainQuantity > 0).ToListAsync();
            DeliveryItemList = new SelectList(deliveryitems, "Id", "DryFoodName");
        }

        public async Task<IActionResult> OnPost()
        {
            var item = await _db.DryFoodDonation.Where(d => d.Id == DeliveryItem.DryFoodID).FirstOrDefaultAsync();
            var max = item.DryFoodRemainQuantity;
            if (DeliveryItem.Quantity < 0 || DeliveryItem.Quantity > max)
            {
                ModelState.AddModelError("DeliveryItem.Quantity", "The quantity of the delivery item must be between 1 and "+max+".");
            }

            if (ModelState.IsValid)
            {
                await _db.DeliveryItem.AddAsync(DeliveryItem);
                var dryfood = _db.DryFoodDonation.Find(DeliveryItem.DryFoodID);
                dryfood.DryFoodRemainQuantity+= Quantity;
                dryfood.DryFoodRemainQuantity -= DeliveryItem.Quantity;
                await _db.SaveChangesAsync();
                TempData["success"] = "Delivery Item added successfully";
                return RedirectToPage("DeliveryItem?id="+DeliveryId);
            }
            return Page();
        }
    }
}
