using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Zero_Hunger.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zero_Hunger.Model
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        //[ForeignKey("FK_User_ToUserType")][Required]
        //public int? UserType { get; set; }
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual UserType UserType{ get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserPwd { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserPhone { get; set; }
        public DateTime UserBirth { get; set; }
        public string UserAdrs1 { get; set; }
        public string UserAdrs2 { get; set; }
        //public virtual ICollection<Delivery>? Deliveries { set; get; }
        //public string DonorType { get; set; }
        public bool RememberMe { get; set; }
    }
}
