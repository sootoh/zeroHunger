using ZeroHunger.Data;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZeroHunger.Pages.Deliveries
{
    public class DeliveryItemModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        
        public int DeliveryId { get; set; }
        [BindProperty]
        public Delivery Delivery { set; get; }
        public DeliveryItem DeliveryItem { set; get; }
        public IEnumerable<DeliveryItem> DeliveryItems { set; get; }
        public IEnumerable<DryFoodDonation> DryFoods { set; get; }

        public DeliveryItemModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            DeliveryId = id;
            //Delivery = _db.Delivery.Find(id);
            DeliveryItems = _db.DeliveryItem.Where(d => d.DeliveryID == id).ToList();
            DryFoods = _db.DryFoodDonation;
        }

        public async Task<IActionResult> OnPost(int itemid)
        {
            DeliveryItem = await _db.DeliveryItem.FirstOrDefaultAsync(d=>d.ItemID == itemid);
            List<DryFoodDonation> dfdlist = await _db.DryFoodDonation.ToListAsync();
            var dryfood = dfdlist.Where(d => d.Id == DeliveryItem.DryFoodID).FirstOrDefault();
            //var dryfood = await _db.DryFoodDonation.FirstOrDefaultAsync(d=>d.Id == DeliveryItem.DryFoodID);
            dryfood.DryFoodRemainQuantity += DeliveryItem.Quantity;
            _db.DryFoodDonation.Update(dryfood);
            _db.DeliveryItem.Remove(DeliveryItem);
            await _db.SaveChangesAsync();
            TempData["success"] = "Delivery Item deleted successfully";
            return Page();
        }
    }
}
