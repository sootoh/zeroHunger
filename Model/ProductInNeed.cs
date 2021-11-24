using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zeroHunger.Model
{
    public class ProductInNeed
    {
        [Key]
        public int product_id { get; set; }
        [Required]
        public string product_name { get; set; }
        public string product_description { get; set; }
        [Required]
        public int amount { get; set; }
        
        [Required]
        public string visibility { get; set; }
        [DisplayName("Image")]
        public string image { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }



    }
}
