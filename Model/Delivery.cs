using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using ZeroHunger.Model;

namespace ZeroHunger.Model
{
    /*public class Categorgory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name="Display Order")]
        [Range(1,100,ErrorMessage="Display order should in range of 1 - 100!!!")]
        public int DisplayOrder { get; set; }
    }*/
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
        public int? UserID { get; set; }
        public int? DryFoodID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        [ForeignKey("DryFoodID")]
        public virtual ICollection<DryFoodDonation> DryFoodDonations { get; set; }
    }
}
