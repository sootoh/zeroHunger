using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace ZeroHunger.Model
{
    [Table("DryFoodDonation")]
    public class DryFoodDonation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DryFoodName { get; set; }
        [Required,]
        public int DryFoodQuantity { get; set; }
        public int DryFoodRemainQuantity { get; set; }
        [Required]
        public string DryFoodPickDate { get; set; }
        [Required]
        public string DryFoodRemark { get; set; }
        
        [ForeignKey("DonorId")]
        public virtual User donor_Id { get; set; }
        [ForeignKey("DelivererId")]
        public virtual User deliverer_Id { get; set; }
        



    }


}
