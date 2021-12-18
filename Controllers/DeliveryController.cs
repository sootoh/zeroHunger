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

namespace ZeroHunger.Controllers
{
    [Route("api/delivery")]
    [ApiController]
    public class DeliveryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private User loginUser;

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
                return Json(new { data = await _db.Delivery.ToListAsync() });
            

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