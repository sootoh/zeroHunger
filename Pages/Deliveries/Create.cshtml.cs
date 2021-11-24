using ZeroHunger.Data;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace ZeroHunger.Pages.Deliveries
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Delivery Delivery { set; get; }

        //public List<SelectListItem> Volunteer { get; set; }
        //public int SelectedVolunteerID { get; set; }
        public SelectList VolunteerList { get; set; }
        //public SelectList ReceiverList { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task onGet()
        {
            //PopulateVolunteersDropDownList(_db);
            /*var items = _db.User.Select(u => new SelectListItem
            {
                Value = u.UserID.ToString(),
                Text = u.UserName
            }).ToList();*/
            //ViewData["Items"] = items;
            var items = await _db.User.Where(u => u.UserType == 2).ToListAsync();
            VolunteerList = new SelectList(_db.User, "UserID", "UserName");
            /*var bitems = await _db.User.Where(u => u.UserType == 3).ToListAsync();
            ReceiverList = new SelectList(bitems, "UserID", "UserName");*/
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
