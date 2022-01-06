using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.ReceiverAccountApplication
{
    public class RejectReceiverAccountApplicationModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public RejectReceiverAccountApplicationModel(ApplicationDbContext db)
        {
            _db = db;

        }
        public Receiver application { get; set; }
        public string message { get; set; } = "";

        public static void SendEmail(string emailbody, string userEmail)
        {
            // Specify the from and to email address
            MailMessage mailMessage = new MailMessage("vtechzerohunger@gmail.com", userEmail);
            // Specify the email body
            mailMessage.Body = emailbody;
            // Specify the email Subject
            mailMessage.Subject = "Sorry! Your application is rejected!";

            // Specify the SMTP server name and post number
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            // Specify your gmail address and password
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "vtechzerohunger@gmail.com",
                Password = "ad_0hunger"
            };
            // Gmail works on SSL, so set this property to true
            smtpClient.EnableSsl = true;
            // Finall send the email message using Send() method
            smtpClient.Send(mailMessage);
        }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            application = new Receiver();
            if (id != null)
            {
                application = _db.Receiver.Where(i => i.receiverIC.Equals(id)).Single();
                foreach (ReceiverFamily familyInfo in _db.ReceiverFamily)
                {
                    if (familyInfo.receiverIC.Equals(id))
                    {
                        _db.ReceiverFamily.Remove(familyInfo);
                    }
                }
                string email = application.receiverEmail;

                _db.Receiver.Remove(application);

                await _db.SaveChangesAsync();

                /*string emailBody = "We appreciate you taking the time to apply as a receiver to ZeroHunger. "
                    + "However, in order to make the best use of the resources available to us, we had to make the painful decision to refuse your application.";

                SendEmail(emailBody, email);*/

                message = "The application is removed. An email is sent to the applicant's e-mail address.";
                TempData["alertMessage"] = "The application is approved. An email is sent to the applicant." +
                    "email: " + application.receiverEmail;
                return RedirectToPage("Index");
            }
            message = "Error!";
            return RedirectToPage();

        }
    }
}
