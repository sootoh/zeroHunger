using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ZeroHunger.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace ZeroHunger.Controllers
{
    [Route("api/Delivery")]
    [ApiController]
    public class DeliveryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public Delivery delivery { get; set; }
        public IEnumerable<Delivery> deliveries { get; set; }

        public DeliveryController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            /*if (@User.Identity.Name != null)
            {
                loginUser = (_db.User.Where(b => b.UserEmail.Equals(@User.Identity.Name)).FirstOrDefault());
                return Json(new { data = await _db.Delivery.Where(c => c.DonorUserID.Equals(loginUser.UserID)).ToListAsync() });

            }
            else
            {*/
            //var volunteername =  _db.Delivery.Include(d => d.Receiver).ToListAsync(); , volunteer = volunteername 

            deliveries = await _db.Delivery.ToListAsync();
            //int.TryParse(HttpContext.Session.GetString("userid"), out uid);
            //CR = await _db.CookReservation.Where(b => b.userId.Equals(uid)).ToListAsync();
            foreach (var item in deliveries)
            {

                item.Volunteer = await _db.User.FindAsync(item.VolunteerID);
                item.Receiver = await _db.User.FindAsync(item.ReceiverID);
            }
            
            return Json(new { data = deliveries });

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var deliveryFromDb = _db.Delivery.SingleOrDefaultAsync(d => d.DeliveryID == id);
            if (deliveryFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            List<DeliveryItem> itemlist = await _db.DeliveryItem.Where(d=>d.DeliveryID == id).ToListAsync();
            foreach(var item in itemlist)
            {
                _db.DeliveryItem.Remove(item);
            }
            _db.Delivery.Remove(await deliveryFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
    }
}
