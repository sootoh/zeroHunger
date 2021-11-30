using ZeroHunger.Data;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZeroHunger.Model;
using System.Collections.Generic;

namespace ZeroHunger.Pages.Deliveries
{
    public class VolunteerDeliveryModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<Delivery> MyDeliveries { set; get; }
        [BindProperty]
        public Delivery Delivery { set; get; }
        public VolunteerDeliveryModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            MyDeliveries = _db.Delivery.Include(d => d.Receiver).Where(r => r.VolunteerID == 2);             
        }

        public async Task<IActionResult> OnPostAccept(int id)
        {
            Delivery = await _db.Delivery.FindAsync(id);
            Delivery.DeliveryStatus = (DeliveryStatus)1;
            _db.Delivery.Update(Delivery);
            await _db.SaveChangesAsync();
            TempData["success"] = "Delivery request accepted successfully";
             return RedirectToPage("VolunteerDelivery");
        }

        public async Task<IActionResult> OnPostComplete(int id)
        {
            Delivery = await _db.Delivery.FindAsync(id);
            Delivery.DeliveryStatus = (DeliveryStatus)2;
            _db.Delivery.Update(Delivery);
            await _db.SaveChangesAsync();
            TempData["success"] = "Delivery request completed successfully";
            return RedirectToPage("VolunteerDelivery");
        }
    }
}
