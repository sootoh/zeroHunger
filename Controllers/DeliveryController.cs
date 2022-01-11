using Microsoft.AspNetCore.Mvc;
using ZeroHunger.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Linq;

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
            deliveries = await _db.Delivery.OrderByDescending(d => d.DeliveryTime).ToListAsync();
            int i = 1;
            foreach (var delivery in deliveries)
            {
                if (delivery.DeliveryStatus.Equals(4))
                {
                    var id = delivery.DeliveryID.ToString();
                    ViewData["Message" + i.ToString()] = "Delivery with ID " + id + " is rejected. Please find a new volunteer.";
                    i++;
                }
            }
            foreach (var delivery in deliveries)
            {
                if (_db.DeliveryItem.Find(delivery.DeliveryID) == null)
                {
                    var delid = delivery.DeliveryID.ToString();
                    ViewData["Message" + i.ToString()] = "Delivery with ID " + delid + " has no delivery item. Please add delivery item now.";
                    i++;
                }
            }
            foreach (var item in deliveries)
            {
                item.Volunteer = await _db.User.FindAsync(item.VolunteerID);
                item.Receiver = await _db.User.FindAsync(item.ReceiverID);
            }
            return Json(new { data = deliveries });
        }
        /*
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> CheckDeliveryItem_Request()
        {
           deliveries = await _db.Delivery.OrderByDescending(d => d.DeliveryTime).ToListAsync();
            int i = 1;
            foreach(var delivery in deliveries)
            {
                if (delivery.DeliveryStatus.Equals(4))
                {
                    var id = delivery.DeliveryID.ToString();
                    ViewData["Message" + i.ToString()] = "Delivery with ID " + id + " is rejected. Please find a new volunteer.";
                    i++;
                }
            }
            foreach(var delivery in deliveries)
            {
                if (_db.DeliveryItem.Find(delivery.DeliveryID) == null)
                {
                    var delid = delivery.DeliveryID.ToString();
                    ViewData["Message" + i.ToString()] = "Delivery with ID " + delid + " has no delivery item. Please add delivery item now.";
                    i++;
                }
            }
            return View();
        }*/
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var deliveryFromDb = await _db.Delivery.FirstOrDefaultAsync(d => d.DeliveryID == id);
            if (deliveryFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            /*List<DeliveryItem> itemlist = await _db.DeliveryItem.Where(d=>d.DeliveryID == id).ToListAsync();
            foreach(var item in itemlist)
            {
                _db.DeliveryItem.Remove(item);
            }*/
            List<DeliveryItem> itemList = await _db.DeliveryItem.ToListAsync();
            itemList.RemoveAll(i => i.DeliveryID == id);
            _db.Delivery.Remove(deliveryFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
