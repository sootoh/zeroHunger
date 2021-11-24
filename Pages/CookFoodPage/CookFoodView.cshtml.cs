using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Zero_Hunger.Model;

namespace Zero_Hunger.Pages.CookFoodPage
{
    public class CookFoodViewModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CookFoodViewModel(ApplicationDbContext db)
        {
            _db = db;

        }
        public IEnumerable<CookedFoodDonation> CookedFood { get; set; }
        public async Task OnGet()
        {
            CookedFood = await _db.CookedFoodDonation.ToListAsync();
        }
    }
}
