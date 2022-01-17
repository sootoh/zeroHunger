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
        public int OriID { get; set; }
        public int DeliveryItemID { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        [BindProperty]
        public DeliveryItem OriDeliveryItem { set; get; }
        [BindProperty]
        public DeliveryItem DeliveryItem { set; get; }
        [BindProperty]
        public Delivery Delivery { set; get; }
        public SelectList DeliveryItemList { set; get; }

        public EditDeliveryItemModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync(string itemid, string delid)
        {
            //var itemid = Request.Query["itemid"];
            //var delid = Request.Query["delid"];
            DeliveryItemID= int.Parse(delid);
            int ItemId = int.Parse(itemid);
            OriDeliveryItem = _db.DeliveryItem.Find(ItemId);
            DeliveryItem = _db.DeliveryItem.Find(ItemId);
            Quantity = DeliveryItem.Quantity;
            var deliveryitems = await _db.DryFoodDonation.Where(d => d.DryFoodRemainQuantity > 0).ToListAsync();
            DeliveryItemList = new SelectList(deliveryitems, "Id", "DryFoodName");
        }
        public async Task<IActionResult> OnPost()
        {
            
            DeliveryItem DeliveryItemFromDB = await _db.DeliveryItem.FindAsync(OriDeliveryItem.ItemID);//get ori delivery item from Db
            if (ModelState.IsValid)
            {
                if (DeliveryItemFromDB.DryFoodID != DeliveryItem.DryFoodID)//if dry food changed
                {
                    var oridryfood = _db.DryFoodDonation.Find(DeliveryItemFromDB.DryFoodID);//get original dry food
                    oridryfood.DryFoodRemainQuantity += DeliveryItem.Quantity;//add back quantity
                    DeliveryItemFromDB.DryFoodID = DeliveryItem.DryFoodID;//change to new dry food
                    DeliveryItemFromDB.Quantity = DeliveryItem.Quantity;//set new quantity
                    var newdryfoodd = _db.DryFoodDonation.Find(DeliveryItemFromDB.DryFoodID);//get new dry food
                    newdryfoodd.DryFoodRemainQuantity -= Quantity;//minus new dry food with quantity
                    await _db.SaveChangesAsync();//save db.
                }
                else//if dry food not changed
                {
                    
                    DryFoodDonation dfdFromDb = await _db.DryFoodDonation.FindAsync(DeliveryItem.DryFoodID);//get dry food
                    dfdFromDb.DryFoodRemainQuantity += DeliveryItemFromDB.Quantity;//add back based on ori quantity
                    dfdFromDb.DryFoodRemainQuantity -= DeliveryItem.Quantity;//delete based on new quantity
                    DeliveryItemFromDB.Quantity = DeliveryItem.Quantity;//set  quantity
                    await _db.SaveChangesAsync();

                }
                
                
                
                var newdryfood = _db.DryFoodDonation.Find(DeliveryItem.DryFoodID);
                newdryfood.DryFoodRemainQuantity -= DeliveryItem.Quantity;
                
                //TempData["success"] = "Delivery Item updated successfully";
                return RedirectToPage("DeliveryItem", new { id = DeliveryItemFromDB.DeliveryID });
            }
            return Page();
        }
    }
}
