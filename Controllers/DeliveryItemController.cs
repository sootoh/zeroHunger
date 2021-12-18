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
    [Route("api/DeliveryItem")]
    [ApiController]
    public class DeliveryItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Delivery Delivery { set; get; }
        [BindProperty]
        public DeliveryItem DeliveryItem { set; get; }
        public IEnumerable<User> Users { set; get; }

        public SelectList DeliveryItemList { set; get; }

        public SelectList VolunteerList { get; set; }

        public SelectList ReceiverList { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        public DeliveryItemController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }
        /*[HttpGet]
        public async Task<IActionResult> GetAll(int id)
        {
            /*if (@User.Identity.Name != null)
            {
                loginUser = (_db.User.Where(b => b.UserEmail.Equals(@User.Identity.Name)).FirstOrDefault());
                return Json(new { data = await _db.CookedFoodDonation.Where(c => c.DonorUserID.Equals(loginUser.UserID)).ToListAsync() });

            }
            else
            {
            var deliveryitem = await _db.DeliveryItem.Where(d=>d.ItemID == id).ToListAsync();
            foreach(var item in deliveryitem)
            {
                
            }
            foreach(var obj in Model.DeliveryItems){
            < tr >
             @foreach(var dryfood in Model.DryFoods){
                    if (@dryfood.Id == @obj.DryFoodID)
                    {
                        var dryfoodname = @dryfood.DryFoodName;
                        < td > @dryfoodname </ td >
                          break;
                    }
                }
                return Json(new { data = await _db.CookedFoodDonation.ToListAsync() });

            }
        }*/
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> SetMaxQuantity(int id)
        {
            List<DryFoodDonation> dfdlist = await _db.DryFoodDonation.ToListAsync();
            var dryfood = dfdlist.Where(d => d.Id == id).FirstOrDefault();
            return new JsonResult(dryfood.DryFoodRemainQuantity);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetPhoneAdrs(int id)
        {
            List<User> receiverlist = await _db.User.Where(u => u.UserType.TypeID == 3).ToListAsync();
            var idUser = _db.User.Where(i => i.UserID == id);
            var selected = _db.User.Where(s => receiverlist.Contains((User)idUser));
            var selectreceiver = receiverlist.Where(d => d.UserID.Equals(id)).FirstOrDefault();
            return new JsonResult(selectreceiver);
        }

    }
}