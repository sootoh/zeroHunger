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
        public User user { get; set; }
        public string message { get; set; } = "";

        public async Task OnGetAsync(string? id)
        {
            application= new Receiver();
            if(id!=null)
            {
                application = _db.Receiver.Where(i => i.receiverIC.Equals(id)).Single();
                application.applicationStatusID = 2;

                user.UserType = (_db.UserType.Where(b => b.TypeID.Equals(1)).FirstOrDefault());
                user.UserName = application.receiverName;
                user.UserAdrs1 = application.receiverAdrs1;
                user.UserAdrs2=application.receiverAdrs2;
                user.UserPhone = application.receiverPhone;

                await _db.User.AddAsync(user);

                application.user=_db.User.Where(i=>i.UserEmail.Equals(application.receiverEmail)).FirstOrDefault();
                application.userID = application.user.UserID;
                _db.Receiver.Update(application);

                await _db.SaveChangesAsync();

                message = "The application is approved. An email is sent to the applicant's e-mail address. " +
                    "email: " + application.receiverEmail + " " +
                    "password: " + "123";
            }
            

        }
    }
}
