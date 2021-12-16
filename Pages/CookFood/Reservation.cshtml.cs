using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.CookFood
{

    public class ReservationModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public ReservationModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<CookedFoodDonation> CFD { get; set; }
        public IEnumerable<CookReservation> CR { get; set; }
        public IEnumerable<CookReservation> CRP { get; set; }


        public async Task OnGet()
        {
            int uid;
            int.TryParse(HttpContext.Session.GetString("userid"), out uid);
            CR = await _db.CookReservation.Where(b => b.userId.Equals(uid)).ToListAsync();
            foreach (var item in CR)
            {
                item.reservationRefCook = await _db.CookedFoodDonation.FindAsync(item.cookId);
            }
            CRP = CR.Where(c => c.status.Equals("Pending"));

            CR = CR.Where(c => c.status.Equals("Success") || c.status == "Expired");

        }
        public async Task<IActionResult> OnPostComplete(int id)
        {
            var CookReservation = await _db.CookReservation.FindAsync(id);
            if (CookReservation == null)
            {
                return NotFound();
            }
            CookReservation.status = "Success";
            await _db.SaveChangesAsync();
            return RedirectToPage("Reservation");
        }
        public async Task<IActionResult> OnPostCancel(int id)
        {
            CookReservation Cookreservation = await _db.CookReservation.FindAsync(id);
            if (Cookreservation == null)
            {
                return NotFound();
            }
            var cfd = await _db.CookedFoodDonation.FindAsync(Cookreservation.cookId);
            cfd.Reservation = cfd.Reservation - 1;
            _db.CookReservation.Remove(Cookreservation);
            await _db.SaveChangesAsync();
            return RedirectToPage("Reservation");
        }


    }
}
