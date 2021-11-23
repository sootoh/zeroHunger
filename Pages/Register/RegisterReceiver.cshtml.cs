using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.Register
{
    public class RegisterReceiverModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public RegisterReceiverModel(ApplicationDbContext db)
        {
            _db = db;

        }
        [BindProperty]
        public Receiver application { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string street { get; set; }
        public string additional { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string message { get; set; } = "";
        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPost(Receiver Application, string fName, string lName, string street, string additional, string zip, string city, string state)
        {
            foreach(Receiver receiver in _db.Receiver)
            {
                if(Application.receiverIC==receiver.receiverIC)
                {
                    message = "This IC no. has been registered.";
                    return Page();
                }
            }
            foreach (ReceiverFamily receiverfamily in _db.ReceiverFamily)
            {
                if (Application.receiverIC == receiverfamily.receiverIC)
                {
                    message = "This IC no has been registered.";
                    return Page();
                }
            }

            Application.receiverName = fName + lName;
            if(!street.EndsWith(','))
            {
                street += ",";
            }
            if (!additional.EndsWith(','))
            {
                additional += ",";
            }
            Application.receiverAdrs1 = street+" " + additional;
            Application.receiverAdrs2 = zip + " " + city + ", " + state;

            if(Application.receiverFamilyNo == 0)
            {
                await _db.Receiver.AddAsync(application);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage("RegisterReceiver_Family", Application);
            }
            
        }
    }

}

