using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
    public class EquipmentTypes {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [StringLength(255)]
        [Index(IsUnique = true)]
        public string Eqmt_Code { get; set; }
        public string Eqmt_Description { get; set; }
        public bool IsCaculated { get; set; }
        public bool IsDeleted { get; set; }
    }
}
