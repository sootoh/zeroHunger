using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages
{
    public class profileModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public profileModel(ApplicationDbContext db)
        {
            _db = db;

        }

        [BindProperty]
        public IEnumerable<User> Users { get; set; }
        public User loginUser { get; set; }
        public string name { get; set; } = "";
        public string email { get; set; } = "";
        public string address1 { get; set; } = "";
        public string address2 { get; set; } = "";
        public string birth { get; set; }
        public string phone{ get; set; } = "";
        public void OnGet()
        {
            loginUser = _db.User.Where(b => b.UserEmail.Equals(@User.Identity.Name)).FirstOrDefault();
            name = loginUser.UserName;
            email = loginUser.UserEmail;
            address1 = loginUser.UserAdrs1;
            address2 = loginUser.UserAdrs2;
            birth = loginUser.UserBirth.ToString("dd/MM/yyyy");
            phone = loginUser.UserPhone;

        }
    }
}
