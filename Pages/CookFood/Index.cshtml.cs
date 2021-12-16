using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.CookFood
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<CookedFoodDonation> CFD { get; set; }

        public int count { get; set; }
        public int id { get; set; }

        public async Task<IActionResult> OnGet()
        {

            string uids=HttpContext.Session.GetString("userid");
            if(uids==null)
            {
                return RedirectToPage("../login");
            }
            int uid;
            int.TryParse(uids, out uid);
            var obj = _db.CookReservation.Where(a => a.userId == uid && a.status.Equals("Pending")).FirstOrDefault();
            Debug.WriteLine(obj == null);
            if (obj==null)
            { 
            CFD = await _db.CookedFoodDonation.ToListAsync();
            count = CFD.Count();
            Debug.WriteLine(count);
            List<CookedFoodDonation> CFDL = new List<CookedFoodDonation>();
            CFDL = CFD.Cast<CookedFoodDonation>().ToList();
           
            CFDL.RemoveAll(x => x.RemainQuantity == 0);
            CFDL.RemoveAll(x => x.CloseDate <= DateTime.Now);
            CFD = (IEnumerable<CookedFoodDonation>)CFDL;
            Debug.WriteLine(CFDL.Count());
                return Page();
            }
            else
            {
                return RedirectToPage("Reservation");
            }
        }

        public async Task OnPost(int ID)
        {
             
            CookReservation crs = new CookReservation();
            var cfd = await _db.CookedFoodDonation.FindAsync(ID);
            if (cfd.Reservation < cfd.RemainQuantity)
            {
                cfd.Reservation = cfd.Reservation + 1;
                crs.date = System.DateTime.Now.ToString("g");
                crs.reservationRefUser = (_db.User.Where(b => b.UserEmail.Equals(@User.Identity.Name)).FirstOrDefault());
                crs.status = "Pending";
                crs.userId = crs.reservationRefUser.UserID;
                crs.reservationRefCook = (await _db.CookedFoodDonation.FindAsync(ID));
                crs.cookId = crs.reservationRefCook.CookID;
                await _db.CookReservation.AddAsync(crs);
                await _db.SaveChangesAsync();
                CFD = await _db.CookedFoodDonation.ToListAsync();
                Response.Redirect("CookFood");
            }
            else
            {
                Response.Redirect("CookFood");
            }
            
        }
    }
}
