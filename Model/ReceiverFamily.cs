using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroHunger.Models
{
    public class ReceiverFamily
    {
        [Key]
        [MaxLength(12)]
        public string familyIC { get; set; }
        [ForeignKey("FK_ReceiverFamily_ToReceiver")] [Required]
        public string receiverIC { get; set; }
        [Required]
        public string familyDOB { get; set; }
        [Required]
        public string familyOccupation { get; set; }
        [ForeignKey("FK_ReceiverFamily_ToSalaryGroup")][Required]
        public int familySalaryGroupID { get; set; }
    }
}
