using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.Register
{
    public class RegisterReceiver_FamilyModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public RegisterReceiver_FamilyModel(ApplicationDbContext db)
        {
            _db = db;

        }
        [BindProperty]
        public IList<ReceiverFamily> applicationFamily { get; set; } = new List<ReceiverFamily>();
        public Receiver application { get; set; }
        public int noFamily { get; set; }
        public string message { get; set; } = "";
        public void OnGet(Receiver Application)
        {
            application = Application;
            noFamily = application.receiverFamilyNo;
            applicationFamily=new ReceiverFamily[noFamily];
        }
        public async Task<IActionResult> OnPost(IList<ReceiverFamily> ApplicationFamily)
        {
            foreach(ReceiverFamily Application in ApplicationFamily)
            {
                foreach (Receiver receiver in _db.Receiver)
                {
                    if (Application.receiverIC == receiver.receiverIC)
                    {
                        message = "This IC no. has been registered.";
                        return Page();
                    }
                }
                foreach (ReceiverFamily receiverfamily in _db.ReceiverFamily)
                {
                    if (Application.receiverIC == receiverfamily.receiverIC)
                    {
                        message = "This IC no. has been registered.";
                        return Page();
                    }
                }
            }
            await _db.Receiver.AddAsync(application);
            foreach (ReceiverFamily receiverFamily in applicationFamily)
            {
                receiverFamily.receiverIC = application.receiverIC;
                await _db.ReceiverFamily.AddAsync(receiverFamily);
            }
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");

        }
    }
}
