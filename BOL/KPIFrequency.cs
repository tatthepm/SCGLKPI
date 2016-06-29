using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
    public class KPIFrequency {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(5)]
        [Index(IsUnique = true)]
        public string KPIFrequencyId { get; set; }

        public string KPIFrequencyName { get; set; }


        public virtual ICollection<MenuTable> MenuTables { get; set; }

        public KPIFrequency() {
            MenuTables = new HashSet<MenuTable>();
        }
    }
}
