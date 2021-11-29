using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Helpers;
using ZeroHunger.Model;

namespace Zero_Hunger.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public User user { get; set; }
        public int? age { get; set; }

        public string username { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            //ID = HttpContext.Session.GetString("Name");
            HttpContext.Session.SetInt32("age", 20);
            HttpContext.Session.SetString("username", "abc");
            SessionHelper.SetObjectAsJson(HttpContext.Session, "user", user);
            age = HttpContext.Session.GetInt32("age");
            username = HttpContext.Session.GetString("username");
            user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user");
        }
    }
}
