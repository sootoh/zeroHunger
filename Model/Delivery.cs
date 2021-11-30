using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zero_Hunger.Model;

namespace ZeroHunger.Model
{
    public class Delivery
    {
        [Key]
        [Display(Name = "Delivery ID")]
        public int DeliveryID { get; set; }
        [Display(Name = "Delivery Status")]
        public DeliveryStatus DeliveryStatus { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Delivery Time")]
        public DateTime? DeliveryTime { get; set; }
        public int? VolunteerID { get; set; }
        public int? ReceiverID { get; set; }
        //public int? DryFoodID { get; set; }
        [ForeignKey("VolunteerID")]
        public virtual User? Volunteer { get; set; }
        [ForeignKey("ReceiverID")]
        public virtual User? Receiver { get; set; }
        //[ForeignKey("DryFoodID")]
        public virtual ICollection<DryFoodDonation>? DryFoodDonations { get; set; }
    }
}
