using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.DryFoodDonationF
{
    public class EditDryFoodDonationModel : PageModel
    {
        private ApplicationDbContext _db;
        public EditDryFoodDonationModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public DryFoodDonation DFD { get; set; }
        public async Task OnGet(int id)
        {
            DFD = await _db.DryFoodDonation.FindAsync(id);

        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var DFDFromDb = await _db.DryFoodDonation.FindAsync(DFD.Id);
                DFDFromDb.DryFoodName = DFD.DryFoodName;
                DFDFromDb.DryFoodQuantity= DFD.DryFoodQuantity;
                DFDFromDb.DryFoodPickDate = DFD.DryFoodPickDate;
                DFDFromDb.DryFoodRemark = DFD.DryFoodRemark;
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage();
            }
        }
        
    }
}
