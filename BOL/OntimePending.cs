﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
    public class OntimePending {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10)]
        public string DELVNO { get; set; }

        [StringLength(10)]
        public string SALEORDNO { get; set; }

        [StringLength(10)]
        public string SHPMNTNO { get; set; }

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
        public DateTime? SHCRDATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ACTGIDATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ACTGIDATE_D { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ORDCMPDATE { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? REQUESTED_DATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PLNONTIMEDATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PLNONTIMEDATE_D { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LOADED_DATE { get; set; }
        [StringLength(4)]
        public string SHPPOINT { get; set; }
        [StringLength(20)]
        public string TRUCK_TYPE { get; set; }
        public int DATEDIFF { get; set; }
    }
}
