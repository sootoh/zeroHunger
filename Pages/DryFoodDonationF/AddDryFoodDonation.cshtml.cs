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
    
    public class AddDryFoodDonationModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public AddDryFoodDonationModel(ApplicationDbContext db)
        {
            _db=db;
        }
        [BindProperty]
        public DryFoodDonation DFD { get; set; }
        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPost(DryFoodDonation dfd)
        {
            dfd.DonorId = "Lalisa";
            if (ModelState.IsValid)
            {
                
                await _db.DryFoodDonation.AddAsync(dfd);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
                
            }
            else
            {
                return Page();
            }
        }
    }
}
