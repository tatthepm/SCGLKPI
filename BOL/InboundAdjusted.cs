﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
    public class InboundAdjusted {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10)]
        public string DELVNO { get; set; }

        [StringLength(10)]
        public string SALEORDNO { get; set; }

        [StringLength(10)]
        public string SHPMNTNO { get; set; }

        [StringLength(32)]
        public string CARRIER_ID { get; set; }

        [StringLength(40)]
        public string VENDOR_CODE { get; set; }

        [StringLength(140)]
        public string VENDOR_NAME { get; set; }

        [StringLength(8)]
        [Index(IsUnique = false)]
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
        public string DEPARTMENT_NAME { get; set; }

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
        public string TO_SHPG_LOC_NAME { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LTNRDDATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LTNRDDATE_D { get; set; } //added

        [Column(TypeName = "datetime2")]
        public DateTime? PLNINBDATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PLNINBDATE_D { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ACTGIDATE { get; set; }

        [Column(TypeName = "datetime2")]
        [Index(IsUnique = false)]
        public DateTime? ACTGIDATE_D { get; set; } //added

        [Column(TypeName = "datetime2")]
        public DateTime? LOADED_DATE { get; set; }

        public int? INB_REASON_ID { get; set; } //added

        public string INB_REASON { get; set; } //added

        public int? INB_ADJUST { get; set; }    //added 

        [StringLength(100)]
        public string INB_ADJUST_BY { get; set; } //added

        [Column(TypeName = "datetime2")]
        public DateTime? INB_ADJUST_DATE { get; set; } //added

        public string INB_REMARK { get; set; } //added
        [StringLength(4)]
        public string SHPPOINT { get; set; }
        [StringLength(20)]
        public string TRUCK_TYPE { get; set; }
    }
}
