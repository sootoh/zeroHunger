using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Zero_Hunger.Model;
using Microsoft.AspNetCore.Authorization;

namespace Zero_Hunger.Pages.UserPage
{
    [Authorize(Policy = "MustBelongToAdmin")]
    public class UserViewModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public UserViewModel(ApplicationDbContext db)
        {
            _db = db;

        }
        public IEnumerable<User> Users { get; set; }
        public async Task OnGet()
        {
            Users = await _db.User.ToListAsync();
        }
    }
}
