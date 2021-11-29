using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZeroHunger.Model;

namespace Zero_Hunger.Pages.CookFoodPage
{
    public class AddCookFoodModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public AddCookFoodModel(ApplicationDbContext db)
        {
            _db = db;  
        }
        [BindProperty]
        public CookedFoodDonation cook { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            //userObj.UserID = "Lalisa";
            if (ModelState.IsValid)
            {
                await _db.CookedFoodDonation.AddAsync(cook);
                await _db.SaveChangesAsync();
                return RedirectToPage("CookFoodView");
            }
            else
            {

                return RedirectToPage("../Index");
            }
        }
        
    }
}
