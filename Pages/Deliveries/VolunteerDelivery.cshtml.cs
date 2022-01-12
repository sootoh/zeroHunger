using ZeroHunger.Data;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;

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
        public void OnGet(int id)
        {
            MyDeliveries = _db.Delivery.Include(d => d.Receiver).Where(r => r.VolunteerID == id).ToListAsync();
            /*
            string userid = HttpContext.Session.GetString("userid");
            if (userid == null)
            {
                return RedirectToPage("../login");
            }
            else
            {*/
            //MyDeliveries = await _db.Delivery.Include(d => d.Receiver).Where(r => r.VolunteerID == int.Parse(id)).ToListAsync();
            /*return Page();
        }*/
        }
        /*
        public async Task<IActionResult> OnPostAccept(int id)
        {
            Delivery = await _db.Delivery.FindAsync(id);
            Delivery.DeliveryStatus = (DeliveryStatus)1;
            _db.Delivery.Update(Delivery);
            await _db.SaveChangesAsync();
            TempData["success"] = "Delivery request accepted successfully";
            ViewData["GetFunction"] = "Accept";
            return RedirectToPage("VolunteerDelivery");
        }

        public async Task<IActionResult> OnPostComplete(int id)
        {
            string userid = HttpContext.Session.GetString("userid");
            if (userid == null)
            {
                return RedirectToPage("../login");
            }
            else
            {
                Delivery = await _db.Delivery.FindAsync(id);
                Delivery.DeliveryStatus = (DeliveryStatus)2;
                _db.Delivery.Update(Delivery);
                await _db.SaveChangesAsync();
                TempData["success"] = "Delivery request completed successfully";
                return RedirectToPage("VolunteerDelivery");
            }
        }
        
        public async Task<IActionResult> OnPostReject(int id)
        {
            string userid = HttpContext.Session.GetString("userid");
            if (userid == null)
            {
                return RedirectToPage("../login");
            }
            else
            {
                Delivery = await _db.Delivery.FindAsync(id);
                Delivery.DeliveryStatus = (DeliveryStatus)4;
                //Delivery.VolunteerID = 0;
                //Delivery.Volunteer = null;
                _db.Delivery.Update(Delivery);
                await _db.SaveChangesAsync();
                TempData["success"] = "Delivery request rejected successfully";
                return RedirectToPage("VolunteerDelivery");
            }
        }*/
    }
}
