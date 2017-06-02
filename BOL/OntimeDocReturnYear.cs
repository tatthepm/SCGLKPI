using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
    public class OntimeDocReturnYear {
        public int Id { get; set; }

        [StringLength(4)]
        public string Year { get; set; }

        [StringLength(5)]
        [Index(IsUnique = false)]
        public string DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        [StringLength(5)]
        [Index(IsUnique = false)]
        public string SectionId { get; set; }

        public string SectionName { get; set; }

        [StringLength(20)]
        public string Segment { get; set; }
        [StringLength(20)]
        public string SubSegment { get; set; }
        public string MatFriGrp { get; set; }

        public string MatName { get; set; }

        public int SumOfDocReturn { get; set; }

        public int OnTime { get; set; }

        public int Delay { get; set; }

        public int AdjustDocReturn { get; set; }

        public int SumOfAdjustDocReturn { get; set; }

        [StringLength(5)]
        public string KpiId { get; set; }

        public string KpiName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LOADED_DATE { get; set; }

        [StringLength(10)]
        [Index(IsUnique = false)]
        public string SoldToId { get; set; }
        [StringLength(800)]
        public string SoldToName { get; set; }
        [StringLength(10)]
        [Index(IsUnique = false)]
        public string CarrierId { get; set; }
        [StringLength(800)]
        public string CarrierName { get; set; }
        public double Percent {
            get {
                if (this.OnTime > 0) {
                    return (double)this.OnTime / (double)this.SumOfDocReturn * 100;
                }
                else {
                    return 0.0;
                }
            }
        }
    }
}
