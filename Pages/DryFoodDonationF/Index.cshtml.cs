using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.DryFoodDonationF
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<DryFoodDonation> DFD { get; set; }
        public async Task OnGet()
        {
            DFD= await _db.DryFoodDonation.ToListAsync();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var DFDs = await _db.DryFoodDonation.FindAsync(id);
            if (DFDs == null)
            {
                return NotFound();
            }
            _db.DryFoodDonation.Remove(DFDs);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
       
    }
}
