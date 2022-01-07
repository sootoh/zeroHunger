using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.ReceiverList
{
    public class ViewReceiverModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public ViewReceiverModel(ApplicationDbContext db)
        {
            _db = db;

        }
        public Receiver receiver { get; set; }
        public IList<ReceiverFamily> family { get; set; } = new List<ReceiverFamily>();
        public IEnumerable<SalaryGroup> salaryGroups { get; set; }
        public ReceiverQuestionnaire questionnaire { get; set; }
        public async Task OnGetAsync(string? id)
        {
            receiver = new Receiver();
            questionnaire = new ReceiverQuestionnaire();
            salaryGroups = _db.SalaryGroup;

            if (id != null)
            {

                receiver = _db.Receiver.Where(i => i.receiverIC.Equals(id)).Single();
                questionnaire = _db.ReceiverQuestionnaire.Where(i => i.receiverIC.Equals(id)).OrderBy(j => j.questionnaireId).LastOrDefault();

                foreach (ReceiverFamily familyInfo in _db.ReceiverFamily)
                {
                    if (familyInfo.receiverIC.Equals(id))
                    {
                        family.Add(familyInfo);
                    }
                }

                if (receiver.applicationStatusID == 3)
                {
                    receiver.applicationStatusID = 2;
                    _db.Receiver.Update(receiver);

                }
                await _db.SaveChangesAsync();
            }

        }
    }
}
