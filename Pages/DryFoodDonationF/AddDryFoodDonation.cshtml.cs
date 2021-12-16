using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> OnGet()
        {
            string uids = HttpContext.Session.GetString("userid");
            if (uids == null)
            {
                return RedirectToPage("../login");
            }
            else
            {
                return Page();
            }
        }
        public async Task<IActionResult> OnPost(DryFoodDonation dfd)
        {
            string uids = HttpContext.Session.GetString("userid");
            int uid;
            int.TryParse(uids, out uid);

            if (ModelState.IsValid)
            {
                dfd.donorid = uid;
                dfd.DryFoodRemainQuantity = dfd.DryFoodQuantity;
                dfd.donor_Id = _db.User.Find(dfd.donorid);
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
