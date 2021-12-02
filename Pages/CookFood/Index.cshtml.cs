using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
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

        public async Task OnGet()
        {

            CFD = await _db.CookedFoodDonation.ToListAsync();
            count = CFD.Count();
            Debug.WriteLine(count);
            List<CookedFoodDonation> CFDL = new List<CookedFoodDonation>();
            CFDL = CFD.Cast<CookedFoodDonation>().ToList();
            CFDL.RemoveAll(x => x.RemainQuantity == 0);
            CFD = (IEnumerable<CookedFoodDonation>)CFDL;
            Debug.WriteLine(CFDL.Count());
        }

        public async Task OnPost(int ID)
        {

            var cfd = await _db.CookedFoodDonation.FindAsync(ID);
            if (cfd.Reservation < cfd.RemainQuantity)
            {
                cfd.Reservation = cfd.Reservation + 1;
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
