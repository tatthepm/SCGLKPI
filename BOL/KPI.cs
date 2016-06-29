using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
    public class KPI {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(5)]
        [Index(IsUnique = true)]
        public string KpiId { get; set; }

        public string KpiName { get; set; }


        public virtual ICollection<MenuTable> MenuTables { get; set; }

        public KPI() {
            MenuTables = new HashSet<MenuTable>();
        }
    }
}
