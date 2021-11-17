using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int DeliveryID { get; set; }
        [Required]
        [DefaultValue("pending")]
        public string Status { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DeliveryTime { get; set; }
        [ForeignKey("ProductID")]
        public int ProductIDFK { get; set; }
        [ForeignKey("ProductName")]
        public string ProductNameFK { get; set; }
        [ForeignKey("ProductQuantity")]
        public int ProductQTFK { get; set; }
    }
}
