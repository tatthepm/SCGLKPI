using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
   public class AcceptedAdjusted {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10)]
        public string SHPMNTNO { get; set; }

        [StringLength(10)]
        public string DELVNO { get; set; }

        [StringLength(32)]
        public string CARRIER_ID { get; set; }

        [StringLength(40)]
        public string VENDOR_CODE { get; set; }

        [StringLength(140)]
        public string VENDOR_NAME { get; set; }

        [StringLength(8)]
        [Index(IsUnique =false)]
        public string MATFRIGRP { get; set; }

        [StringLength(100)]
        public string MATNAME { get; set; }

        [StringLength(5)]
        public string REGION_ID { get; set; }

        [StringLength(30)]
        public string REGION_NAME_TH { get; set; }

        [StringLength(30)]
        public string REGION_NAME_EN { get; set; }

        [StringLength(5)]
        [Index(IsUnique = false)]
        public string DEPARTMENT_ID { get; set; }

        [StringLength(100)]
        public string DEPARTMENT_Name { get; set; }

        [StringLength(5)]
        [Index(IsUnique = false)]
        public string SECTION_ID { get; set; }

        [StringLength(100)]
        public string SECTION_NAME { get; set; }
        [StringLength(20)]
        [Index(IsUnique = false)]
        public string SEGMENT { get; set; }

        [StringLength(20)]
        public string SUBSEGMENT { get; set; }

        [StringLength(10)]
        public string SOLDTO { get; set; }

        [StringLength(800)]
        public string SOLDTO_NAME { get; set; }

        [StringLength(10)]
        public string SHIPTO { get; set; }

        [StringLength(280)]
        public string LAST_SHPG_LOC_NAME { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LTNRDDATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LTNRDDATE_D { get; set; } //added

        [Column(TypeName = "datetime2")]
        public DateTime? PLNACPDDATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PLNACPDDATE_D { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LACPDDATE { get; set; }

        [Column(TypeName = "datetime2")]
        [Index(IsUnique = false)]
        public DateTime? LACPDDATE_D { get; set; } //added

        [Column(TypeName = "datetime2")]
        public DateTime? LOADED_DATE { get; set; }

        public int? ACPD_REASON_ID { get; set; } //added

        public string ACPD_REASON { get; set; } //addded

        public int? ACPD_ADJUST { get; set; } //added

        [StringLength(100)]
        public string ACPD_ADJUST_BY { get; set; } //added

        [Column(TypeName = "datetime2")]
        public DateTime? ACPD_ADJUST_DATE { get; set; } //added

        public string ACPD_REMARK { get; set; }
        [StringLength(4)]
        public string SHPPOINT { get; set; }
        [StringLength(20)]
        public string TRUCK_TYPE { get; set; }
    }
}
