﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BOL {
    public class OntimeOutboundYear {
        public int Id { get; set; }

        [StringLength(4)]
        public string Year { get; set; }

        [StringLength(5)]
        public string DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        [StringLength(5)]
        public string SectionId { get; set; }

        public string SectionName { get; set; }

        public string MatFriGrp { get; set; }

        public string MatName { get; set; }

        public int SumOfOutbound { get; set; }

        public int OnTime { get; set; }

        public int Delay { get; set; }

        public int AdjustOutbound { get; set; }

        public int SumOfAdjustOutbound { get; set; }

        [StringLength(5)]
        public string KpiId { get; set; }

        public string KpiName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LOADED_DATE { get; set; }

        public double Percent {
            get {
                if (this.OnTime > 0) {
                    return (double)this.OnTime / (double)this.SumOfOutbound * 100;
                }
                else {
                    return 0.0;
                }
            }
        }
    }
}
