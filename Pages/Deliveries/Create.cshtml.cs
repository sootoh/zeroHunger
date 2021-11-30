using ZeroHunger.Data;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ZeroHunger.Pages.Deliveries
{
    
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Delivery Delivery { set; get; }
        [BindProperty]
        public List<DryFoodDonation> DryFoodList { set; get; }
     
        public SelectList VolunteerList { get; set; }

        public SelectList ReceiverList { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task OnGetAsync()
        {
            //PopulateVolunteersDropDownList(_db);
            /*var items = _db.User.Select(u => new SelectListItem
            {
                Value = u.UserID.ToString(),
                Text = u.UserName
            }).ToList();
            //ViewData["Items"] = items;*/
            var items = await _db.User.Where(u => u.UserType.TypeID == 2).ToListAsync();
            VolunteerList = new SelectList(items, "UserID", "UserName");
            
            var ritems = await _db.User.Where(u => u.UserType.TypeID == 3).ToListAsync();
            ReceiverList = new SelectList(ritems, "UserID", "UserName");

            DryFoodList = await _db.DryFoodDonation.ToListAsync();
        }
        /*public JsonResult OnGetGetReceiverPhoneAddress(int selectReceiver)
        {
            //based on the selected receiver to filter data.
            return new JsonResult(_db.User.Distinct().Where(c => c.UserID == selectReceiver).Distinct().ToList());
        }
        public void OnGetGetQuantity(int deliveryID, int dryfoodID, int quantity)
        {
            if (quantity > 0)
            {
                string qty = quantity.ToString();
                Delivery del = _db.Delivery.Where(d => d.DeliveryID.Equals(deliveryID)).Single();
                if (del.Quantity != null){
                    del.Quantity += "/" + qty;
                }
                else{
                    del.Quantity = qty;
                }
                DryFoodDonation food = _db.DryFoodDonation.Where(d => d.Id == dryfoodID).Single();
                food.DryFoodRemainQuantity -= quantity;

            }
        }*/
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
