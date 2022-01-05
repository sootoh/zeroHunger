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
    public class ViewReceiverAccountApplicationModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public ViewReceiverAccountApplicationModel(ApplicationDbContext db)
        {
            _db = db;

        }
        public Receiver application { get; set; }
        public IList<ReceiverFamily> family { get; set; } = new List<ReceiverFamily>();
        public IEnumerable<SalaryGroup> salaryGroups { get; set; }
        public ReceiverQuestionnaire questionnaire { get; set; }
        public async Task OnGetAsync(string? id)
        {
            application=new Receiver();
            questionnaire=new ReceiverQuestionnaire();
            salaryGroups = _db.SalaryGroup;

            if (id!=null)
            {

                application = _db.Receiver.Where(i => i.receiverIC.Equals(id)).Single();
                questionnaire=_db.ReceiverQuestionnaire.Where(i=> i.receiverIC.Equals(id)).OrderBy(j=>j.questionnaireId).LastOrDefault();

                foreach(ReceiverFamily familyInfo in _db.ReceiverFamily)
                {
                    if(familyInfo.receiverIC.Equals(id))
                    {
                        family.Add(familyInfo);
                    }
                }

                if(application.applicationStatusID ==0) 
                {
                    application.applicationStatusID = 1;
                    _db.Receiver.Update(application);
                    
                }
                await _db.SaveChangesAsync();
            }

        }
    }
}
