using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages
{
    public class ApproveReceiverAccountApplicationModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public ApproveReceiverAccountApplicationModel(ApplicationDbContext db)
        {
            _db = db;

        }
        public Receiver application { get; set; }
        public string message { get; set; } = "";

        public async Task OnGetAsync(string? id)
        {
            application= new Receiver();
            if(id!=null)
            {
                application = _db.Receiver.Where(i => i.receiverIC.Equals(id)).Single();
                application.applicationStatusID = 2;

                //_db.User.Add(application);
                //int userID = _db.User.Where(i => i.email.Equals(application.receiverEmail)).Single().userID;

                //application.userID = userID;
                _db.Receiver.Update(application);

                await _db.SaveChangesAsync();

                message = "The application is approved. An email is sent to the applicant's e-mail address. " +
                    "email: " + application.receiverEmail + " " +
                    "password: " + "123";
            }
            

        }
    }
}
