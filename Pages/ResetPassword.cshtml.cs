
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ZeroHunger.Pages
{
    public class ResetPasswordModel : PageModel
    {
        public string password{ get; set; } = "";
        public string userEmail { get; set; } = "";
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public User forgotUser { get; set; }
        public ResetPasswordModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task OnGetAsync(int id)
        {
            forgotUser = await _db.User.FindAsync(id);
        }

        public async Task<IActionResult> OnPostAsync(string password)
        {
            forgotUser = await _db.User.FindAsync(forgotUser.UserID);

            if (forgotUser != null)
            {
                forgotUser.UserPwd = password;
                await _db.SaveChangesAsync();
                ViewData["Message"] = string.Format("You password was updated.\\Please try to login.");
                return RedirectToPage("login");
                
            }
            else
            {
                ViewData["Message"] = string.Format("Error occured.\\Please try again.");
                return Page();
            }
        }
    }
}
