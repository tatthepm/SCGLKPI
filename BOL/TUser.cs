using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
   public class TUser {
       [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int UserId { get; set; }

       [Required]
       [StringLength(50)]
       [EmailAddress]
       [Index(IsUnique = true)]
       public string UserEmail { get; set; }

       [Required]
       [DataType(DataType.Password)]
       public string Password { get; set; }

       [Required]
       [DataType(DataType.Password)]
       [Compare("Password")]
       public string ConfirmPassword { get; set; }

       public virtual Role Role { get; set; }
       [ForeignKey("Role")]
       public string RoleId { get; set; }

       public DateTime? LastLogin { get; set; }
    }
}
