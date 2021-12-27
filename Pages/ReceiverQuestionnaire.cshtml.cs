using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.OData.Edm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages
{
    public class ReceiverQuestionnaireModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public ReceiverQuestionnaireModel(ApplicationDbContext db)
        {
            _db = db;

        }
        [BindProperty]
        public IList<ReceiverFamily> receiverFamilies { get; set; } = new List<ReceiverFamily>();
        [BindProperty]
        public ReceiverQuestionnaire questionnaire { get; set; }
        public Receiver receiver { get; set; }
        public bool property_soHouse { get; set; } = false;
        public bool property_soHouseRent { get; set; } = false;
        public string property_soHouseLoan { get; set; }
        public bool property_rHouse { get; set; } = false;
        public string property_rHouseLoan { get; set; }
        public bool car { get; set; } = false;
        public string carLoan { get; set; }
        public bool motorcycle { get; set; } = false;
        public string motorcycleLoan { get; set; }

        public bool relatives { get; set; } = false;
        public string relativesSponsor { get; set; }
        public bool adopt { get; set; } = false;
        public bool otherSponsorships { get; set; } = false;
        public string otherSponsorshipsSponsor { get; set; }

        public bool partTime { get; set; } = false;
        public bool willingPartTime { get; set; } = false;
        public void OnGet(string id)
        {
            receiver = new Receiver();
            if (id!=null)
            {
                receiver=_db.Receiver.Where(i => i.receiverIC.Equals(id)).Single();

            }
        }

        public async Task<IActionResult> OnPost()
        {
            receiver = new Receiver();
            if(questionnaire!=null)
            {
                receiver = _db.Receiver.Where(i => i.receiverIC.Equals(questionnaire.receiverIC)).Single();
                questionnaire.receiver=receiver;

                questionnaire.otherSponsorship = "";
                if (relatives)
                {
                    questionnaire.otherSponsorship += "Siblings, Friends/Relatives: RM"+relativesSponsor+"/month<br>";
                    if(adopt)
                    {
                        questionnaire.otherSponsorship += "*possible to adopt children<br>";
                    }
                    else
                    {
                        questionnaire.otherSponsorship += "*not possible to adopt children<br>";
                    }
                }
                if(otherSponsorships)
                {
                    questionnaire.otherSponsorship += "Others NGO sponsorship: RM"+otherSponsorshipsSponsor+"/month<br>";
                }

                questionnaire.property = "";
                if(property_soHouse)
                {
                    questionnaire.property += "Self Owned House: RM" + property_soHouseLoan + "/month<br>";
                    if(property_soHouseRent)
                    {
                        questionnaire.property += "*is able to rent out spare room<br>";
                    }
                    else
                    {
                        questionnaire.property += "*is able to rent out spare room<br>";
                    }

                }
                if(property_rHouse)
                {
                    questionnaire.property += "Rented House: RM" + property_rHouseLoan + "/month<br>";
                }
                if(car)
                {
                    questionnaire.property += "Car: RM" + carLoan + "/month<br>";
                }
                if (motorcycle)
                {
                    questionnaire.property += "Motorcycle: RM" + motorcycleLoan + "/month<br>";
                }

                questionnaire.additional = "";
                if(partTime)
                {
                    questionnaire.additional += "15 years old and above children work as part time during weekends/ holiday to support family.<br>";
                }
                else
                {
                    questionnaire.additional += "15 years old and above children do not work as part time during weekends/ holiday to support family.<br>";
                }
                if(willingPartTime)
                {
                    questionnaire.additional += "*Willing to let them work part-time<br>";
                }
                else
                {
                    questionnaire.additional += "*Not willing to let them work part-time<br>";
                }
            }

            questionnaire.date = Date.Now.ToString();

            await _db.ReceiverQuestionnaire.AddAsync(questionnaire);
            
            await _db.SaveChangesAsync();
            return RedirectToPage("/FamilyStatus");
        }
    }
}
