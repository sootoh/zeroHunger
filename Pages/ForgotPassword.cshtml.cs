using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;


namespace ZeroHunger.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        public string emailBody { get; set; } = "";
        public string userEmail { get; set; } = "";
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public User forgotUser { get; set; }
        public ForgotPasswordModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public string resetPasswordLink { get; set; } = "";
        public void OnGet()
        {
           
            
        }

        public void anotherSendEmail(string emailbody, string userEmail)
        {

            var client = new SocketLabsClient(42290, "e5N6HwBb2k8A3Mrc9R7K"); 

            var message = new BasicMessage();

            message.Subject = "Zero Hunger Reset Password";
            message.HtmlBody = emailbody;
            message.PlainTextBody = emailbody;

            message.From.Email = "vtechzerohunger@gmail.com";

            
            message.To.Add(userEmail); 

            var response = client.Send(message);
        }
        public IActionResult OnPost(string userEmail)
        {
            
                
                forgotUser = (User)(_db.User.Where(b => b.UserEmail.Equals(userEmail)).FirstOrDefault());
                
                if (forgotUser != null)
                {

                    resetPasswordLink = "https://zerohunger20211219221449.azurewebsites.net/ResetPassword?p=" + forgotUser.UserID;

                    anotherSendEmail(("Please reset your password <br>" + resetPasswordLink), userEmail);
                    ViewData["Message"] = string.Format("We have sent an email to you.\\Please check your email.");



                    return RedirectToPage("/login");

                }
                
                ViewData["Message"] = string.Format("This email has not been registered yet.\\nPlease register instead.");
                return Page();
            

        }
    }
}
