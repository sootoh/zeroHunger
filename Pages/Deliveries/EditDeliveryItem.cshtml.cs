using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.Deliveries
{
    public class EditDeliveryItemModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public int DeliveryId { get; set; }
        public int quantity { get; set; }
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
            quantity = DeliveryItem.Quantity;
            var deliveryitems = await _db.DryFoodDonation.ToListAsync();
            DeliveryItemList = new SelectList(deliveryitems, "Id", "DryFoodName");
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _db.DeliveryItem.AddAsync(DeliveryItem);
                var dryfood = _db.DryFoodDonation.Find(DeliveryItem.DryFoodID);
                dryfood.DryFoodRemainQuantity+= quantity;
                dryfood.DryFoodRemainQuantity -= DeliveryItem.Quantity;
                await _db.SaveChangesAsync();
                TempData["success"] = "Delivery Item added successfully";
                return RedirectToPage("DeliveryItem");
            }
            return Page();
        }
    }
}
