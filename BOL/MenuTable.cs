using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
    public class MenuTable {

        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(5)]
        public string DepartmentId { get; set; }

        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(5)]
        public string SectionId { get; set; }

        [Key, Column(Order = 2), DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(5)]
        public string KPIId { get; set; }

        [Key, Column(Order = 3), DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(5)]
        public string KPIFrequencyId { get; set; }

        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string KpiName { get; set; }
        public string KPIFrequencyName { get; set; }


        public virtual KPI KPI { get; set; }
        public virtual KPIFrequency KPIFrequency { get; set; }

    }
}
