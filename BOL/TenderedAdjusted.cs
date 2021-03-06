﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
    public class TenderedAdjusted {
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
        //[Index(IsUnique = false)]
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
        //[Index(IsUnique = false)]
        public string DEPARTMENT_ID { get; set; }

        [StringLength(100)]
        public string DEPARTMENT_NAME { get; set; }

        [StringLength(5)]
        //[Index(IsUnique = false)]
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

        public DateTime? SHCRDATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? SHCRDATE_D { get; set; } //added

        [Column(TypeName = "datetime2")]
        public DateTime? PLNTNRDDATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PLNTNRDDATE_D { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? FTNRDDATE { get; set; }

        [Column(TypeName = "datetime2")]
        [Index(IsUnique = false)]
        public DateTime? FTNRDDATE_D { get; set; } //added

        [Column(TypeName = "datetime2")]
        public DateTime? LOADED_DATE { get; set; }

        public int? TNRD_ONTIME_REASON_ID { get; set; } //added

        public string TNRD_ONTIME_REASON { get; set; } //added

        public int? TNRD_ADJUST { get; set; } //added

        [StringLength(100)]
        public string TNRD_ADJUST_BY { get; set; } //added

        [Column(TypeName = "datetime2")]
        public DateTime? TNRD_ADJUST_DATE { get; set; }

        public string TNRD_ONTIME_REMARK { get; set; }
        [StringLength(4)]
        public string SHPPOINT { get; set; }
        [StringLength(20)]
        public string TRUCK_TYPE { get; set; }

        [StringLength(40)]
        public string CRTD_USR_CD { get; set; }

        [StringLength(40)]
        public string UPDT_USR_CD { get; set; }
    }
}
