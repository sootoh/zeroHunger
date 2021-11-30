using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.CookFood
{
    public class ViewCookFoodModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public ViewCookFoodModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<CookedFoodDonation> CFD { get; set; }

        public int count { get; set; }
        public int loopCount { get; set; }
        public async Task OnGet()
        {
            CFD = await _db.CookedFoodDonation.ToListAsync();
            count = CFD.Count();
        }
 
        public async Task<IActionResult> OnPost(int id)
        {
            
            //getUserID
            Debug.WriteLine(id);
            var cfd = await _db.CookedFoodDonation.FindAsync(id);
            cfd.Reservation=cfd.Reservation-1;
            await _db.SaveChangesAsync();
            return Page();
        }
    }
}
