using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Zero_Hunger.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zero_Hunger.Model
{
    public class CookedFoodDonation
    {
        [Key]
        public int CookID { get; set; }
        [Required]
        public string CookName { get; set; }
        [Required]
        public int CookQuantity { get; set; }
        [Required]
        public float CookLongtitude { get; set; }
        [Required]
        public float CookLatitude { get; set; }
        [Required]
        public string DonorId { get; set; }
    }
}
