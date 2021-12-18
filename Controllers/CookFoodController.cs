using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ZeroHunger.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

namespace ZeroHunger.Controllers
{
    [Route("api/cookFood")]
    [ApiController]
    public class CookFoodController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private User loginUser;

        public CookFoodController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (@User.Identity.Name != null)
            {
                loginUser = (_db.User.Where(b => b.UserEmail.Equals(@User.Identity.Name)).FirstOrDefault());
                return Json(new { data = await _db.CookedFoodDonation.Where(c => c.DonorUserID.Equals(loginUser.UserID)).ToListAsync() });

            }
            else
            {
                return Json(new { data = await _db.CookedFoodDonation.ToListAsync() });

            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var cookFromDb = await _db.CookedFoodDonation.FirstOrDefaultAsync(u => u.CookID == id);
            if (cookFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.CookedFoodDonation.Remove(cookFromDb);
            await _db.SaveChangesAsync();
            
            return Json(new { success = true, message = "Delete successful" });
        }
        
        [HttpPost]
        public async Task<IActionResult> End(int id)
        {
            var cookFromDb = await _db.CookedFoodDonation.FirstOrDefaultAsync(u => u.CookID == id);
            cookFromDb.RemainQuantity = 0;
            if (cookFromDb == null)
            {
                return Json(new { success = false, message = "Error while Ending" });
            }
            
            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Ended Successfully" });
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Add(int addID)
        {
            var cookFromDb = await _db.CookedFoodDonation.FirstOrDefaultAsync(u => u.CookID == addID);

            if (cookFromDb == null)
            {
                return Json(new { success = false, message = "Error while Increasing" });
            }
            cookFromDb.RemainQuantity += 1;
            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Increased Successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> Minus(int minusID)
        {
            var cookFromDb = await _db.CookedFoodDonation.FirstOrDefaultAsync(u => u.CookID == minusID);
            
            if (cookFromDb == null)
            {
                return Json(new { success = false, message = "Error while Decreasing" });
            }
            cookFromDb.RemainQuantity -= 1;
            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Decreased Successfully" });
        }
    }
}
