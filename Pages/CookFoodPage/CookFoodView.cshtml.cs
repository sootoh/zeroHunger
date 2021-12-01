using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using ZeroHunger.Model;
using ZeroHunger.Data;

namespace ZeroHunger.Pages.CookFoodPage
{
    public class CookFoodViewModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CookFoodViewModel(ApplicationDbContext db)
        {
            _db = db;

        }
        [BindProperty]
        public IEnumerable<CookedFoodDonation> CookedFood { get; set; }
        public User donor { get; set; }
        public async Task OnGet()
        {
            
            donor= _db.User.Where(x => x.UserEmail.Equals(User.Identity.Name)).FirstOrDefault();
            CookedFood = await _db.CookedFoodDonation.Where(x=> x.DonorUserID.Equals(donor.UserID)).ToListAsync();
           // CookedFood = await _db.CookedFoodDonation.ToListAsync();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var food = await _db.CookedFoodDonation.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            _db.CookedFoodDonation.Remove(food);
            await _db.SaveChangesAsync();
            return RedirectToPage("CookFoodView");
        }
    }
}
