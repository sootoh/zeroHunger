using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZeroHunger.Model
{
    public class CookReservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int cookId { get; set; }
        [Required]
        public string date { get; set; }
        [ForeignKey("resRefCook")]
        public virtual CookedFoodDonation resRefCook { get; set; }

    }
}
