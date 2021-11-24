using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zero_Hunger.Model;

namespace Zero_Hunger.Pages.UserPage
{
    public class CreateUserModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public CreateUserModel(ApplicationDbContext db)
        {
            _db = db;  
        }
        [BindProperty]
        public User user { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            //userObj.UserID = "Lalisa";
            if (ModelState.IsValid)
            {
                await _db.User.AddAsync(user);
                await _db.SaveChangesAsync();
                return RedirectToPage("UserView");
            }
            else
            {

                return RedirectToPage("../Index");
            }
        }
    }
}
