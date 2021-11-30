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
        [BindProperty]
        public User user { get; set; }
        public string message { get; set; } = "";

        public async Task OnGetAsync(string? id)
        {
            application= new Receiver();
            user= new User();
            if(id!=null)
            {
                application = _db.Receiver.Where(i => i.receiverIC.Equals(id)).Single();
                application.applicationStatusID = 2;

                user.UserType = (UserType)(_db.UserType.Where(b => b.TypeID.Equals(1)).FirstOrDefault());
                user.UserName = application.receiverName;
                user.UserPwd = "123";
                user.UserEmail=application.receiverEmail;
                int year = int.Parse(application.receiverDOB.Substring(0, 4));
                int month = int.Parse(application.receiverDOB.Substring(5, 2));
                int day = int.Parse(application.receiverDOB.Substring(8, 2));
                user.UserBirth = new System.DateTime(year,month,day);
                user.UserAdrs1 = application.receiverAdrs1;
                user.UserAdrs2=application.receiverAdrs2;
                user.UserPhone = application.receiverPhone;

                await _db.User.AddAsync(user);
                await _db.SaveChangesAsync();

                application.user=new User();
                application.user=_db.User.Where(i=>i.UserEmail.Equals(application.receiverEmail)).Single();
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
