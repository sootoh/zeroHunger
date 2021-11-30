using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroHunger.Model
{
    public class ReceiverFamily
    {
        [Key]
        [MaxLength(12)]
        public string familyIC { get; set; }
        [ForeignKey("Receiver")] [Required]
        public virtual Receiver receiver { get; set; }
        public string receiverIC { get; set; }
        [Required]
        public string familyDOB { get; set; }
        [Required]
        public string familyOccupation { get; set; }
        [ForeignKey("FamilySalaryGroup")][Required]
        public virtual SalaryGroup familySalaryGroup { get; set; }
        public int familySalaryGroupID { get; set; }
    }
}
