using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.Deliveries
{
    public class AddDeliveryItemModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public int DeliveryId { get; set; }
        [BindProperty]
        public DeliveryItem DeliveryItem { set; get; }
        public Delivery Delivery { set; get; }
        public SelectList DeliveryItemList { set; get; }

        public AddDeliveryItemModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task OnGet(int id)
        {
            DeliveryId = id;
            Delivery = _db.Delivery.Find(id);
            var deliveryitems = await _db.DryFoodDonation.ToListAsync();
            DeliveryItemList = new SelectList(deliveryitems, "Id", "DryFoodName");
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
               
                //DeliveryItem deliveryitem = new DeliveryItem();
                //deliveryitem.DeliveryID = D
                //deliveryitem.DryFoodID = DeliveryItem.DryFoodID;
                //deliveryitem.Quantity = DeliveryItem.Quantity;
               
                await _db.DeliveryItem.AddAsync(DeliveryItem);
                //Delivery.DeliveryItems.Add(DeliveryItem);
                var dryfood = _db.DryFoodDonation.Find(DeliveryItem.DryFoodID);
                dryfood.DryFoodRemainQuantity -= DeliveryItem.Quantity;
                await _db.SaveChangesAsync();
                //DeliveryItem.DryFood.DryFoodRemainQuantity -= DeliveryItem.Quantity;
                TempData["success"] = "Delivery Item added successfully";
                return RedirectToPage("DeliveryItem");
            }
            return Page();
        }
    }
}
