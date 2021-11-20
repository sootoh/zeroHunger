using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroHunger.Models
{
    public class Receiver
    {
        [Key][MaxLength(12)]
        public string receiverIC { get; set; }
        [Required]
        public string receiverName { get; set; }
        [Required]
        public char receiverGender { get; set; }
        [Required]
        public string receiverDOB { get; set; }
        [Required]
        public string receiverOccupation { get; set; }
        [ForeignKey("FK_Receiver_ToSalaryGroup")][Required]
        public int receiverSalaryGroupID { get; set; }
        [Required]
        public int receiverFamilyNo { get; set; }
        [Required]
        public string receiverEmail { get; set; }
        [Required]
        public string receiverPhone { get; set; }
        [Required]
        public string receiverAdrs1 { get; set; }
        [Required]
        public string receiverAdrs2 { get; set; }
        public int applicationStatusID { get; set; } = 0;
        [ForeignKey("FK_Receiver_ToUser")]
        public int? userID { get; set; }

      
    }
}
