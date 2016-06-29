using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
    public class Role {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string RoleId { get; set; }

        [Required]
        [StringLength(160)]
        [Index(IsUnique = true)]
        public string RoleDesc { get; set; }

        public virtual ICollection<TUser> TUsers { get; set; }
        public Role() {
            TUsers = new HashSet<TUser>();
        }
    }
}
