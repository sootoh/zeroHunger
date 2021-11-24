using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Zero_Hunger.Model
{
    public class UserType
    {
        [Key]
        public int typeID { get; set; }
        [Required]
        [MaxLength(255)]
        public string typeName { get; set; }
        [MaxLength(255)]

        public string typeInterface { get; set; }
       
        
    }
}
