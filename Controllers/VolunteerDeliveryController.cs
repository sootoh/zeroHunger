using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ZeroHunger.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ZeroHunger.Controllers
{
    [Route("api/volunteerdelivery")]
    [ApiController]
    public class VolunteerDeliveryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public Delivery Delivery { get; set; }
        public IEnumerable<Delivery> Deliveries { get; set; }

        public VolunteerDeliveryController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            int uid;
            int.TryParse(HttpContext.Session.GetString("userid"), out uid);
            Deliveries = _db.Delivery.Where(r => r.ReceiverID.Equals(uid)).OrderByDescending(d => d.DeliveryTime).Include(d => d.Receiver);
            Deliveries = Deliveries.Where(d => (int) d.DeliveryStatus != 4);
            foreach (var item in Deliveries)
            {
                item.Receiver = await _db.User.FindAsync(item.ReceiverID);
            }
            return Json(new { data = Deliveries });
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Accept(int id)
        {
            Delivery = await _db.Delivery.FindAsync(id);
            if (Delivery == null)
            {
                return Json(new { success = false, message = "Error while Accepting Delivery Request" });
            }
            Delivery.DeliveryStatus = (DeliveryStatus)1;
            _db.Delivery.Update(Delivery);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delivery Request Accepted Successfully" });
            //TempData["success"] = "Delivery request accepted successfully";
            //return RedirectToPage("VolunteerDelivery");
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Complete(int id)
        {
            Delivery = await _db.Delivery.FindAsync(id);
            if (Delivery == null)
            {
                return Json(new { success = false, message = "Error while Completing Delivery Request" });
            }
            Delivery.DeliveryStatus = (DeliveryStatus)2;
            _db.Delivery.Update(Delivery);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delivery Request Completed Successfully" });
            //TempData["success"] = "Delivery request completed successfully";
            //return RedirectToPage("VolunteerDelivery");
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Reject(int id)
        {
            Delivery = await _db.Delivery.FindAsync(id);
            if (Delivery == null)
            {
                return Json(new { success = false, message = "Error while Rejecting Delivery Request" });
            }
            Delivery.DeliveryStatus = (DeliveryStatus)4;
            //Delivery.VolunteerID = 0;
            //Delivery.Volunteer = null;
            _db.Delivery.Update(Delivery);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delivery Request Rejected Successfully" });
            //TempData["success"] = "Delivery request rejected successfully";
            //return RedirectToPage("VolunteerDelivery");
        }
    }
}
