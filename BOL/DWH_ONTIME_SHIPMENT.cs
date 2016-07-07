using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
    public class DWH_ONTIME_SHIPMENT {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10)]
        public string SHPMNTNO { get; set; }

        [StringLength(50)]
        public string DELVNO { get; set; }

        [StringLength(3)]
        public string WORK_TYPE { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ACTGIDATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ACTGIDATE_D { get; set; } //added

        [StringLength(5)]
        public string FLEET_ID { get; set; }

        [StringLength(3)]
        public string SEGMENT_ID { get; set; }

        [StringLength(8)]
        public string MATFRIGRP { get; set; }

        public string MATNAME { get; set; }

        [StringLength(32)]
        public string CARRIER_ID { get; set; }

        [StringLength(5)]
        public string REGION_ID { get; set; }

        [StringLength(4)]
        public string REGION_ID_SAP { get; set; }

        [StringLength(10)]
        public string SOLDTO { get; set; }
        [StringLength(800)]
        public string SOLDTO_NAME { get; set; }

        [StringLength(5)]
        [Index("DN_SECTION", 5, IsUnique = false)]
        public string SECTION_ID { get; set; }

        [StringLength(100)]
        public string SECTION_NAME { get; set; }

        [StringLength(10)]
        public string SHIPTO { get; set; }

        [StringLength(4)]
        public string SHPPOINT { get; set; }

        [StringLength(3)]
        public string INCOTERM { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? SHCRDATE { get; set; }

        [Column(TypeName = "datetime2")]
        [Index("DN_SHCRDATE",1,IsUnique = false)]
        public DateTime? SHCRDATE_D { get; set; } //added

        [Column(TypeName = "datetime2")]
        public DateTime? FTNRDDATE { get; set; }

        [Column(TypeName = "datetime2")]
        [Index("DN_FTNRDDATE", 3, IsUnique = false)]
        public DateTime? FTNRDDATE_D { get; set; } //added

        [Column(TypeName = "datetime2")]
        public DateTime? LTNRDDATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LTNRDDATE_D { get; set; } //added

        [Column(TypeName = "datetime2")]
        public DateTime? LACPDDATE { get; set; }

        [Column(TypeName = "datetime2")]
        [Index("DN_LACPDDATE", 2, IsUnique = false)]
        public DateTime? LACPDDATE_D { get; set; } //added

        public int? TNRD_ONTIME { get; set; }

        public int? TNRD_COUNT { get; set; }

        public int? TNRD_ONTIME_REASON_ID { get; set; } //added
        public string TNRD_ONTIME_REASON { get; set; } //added
        public int? TNRD_ADJUST { get; set; } //added
        [StringLength(100)]
        public string TNRD_ADJUST_BY { get; set; } //added
        [Column(TypeName = "datetime2")]
        public DateTime? TNRD_ADJUST_DATE { get; set; }
        public string TNRD_ONTIME_REMARK { get; set; }

        public int? ACPD_ONTIME { get; set; }

        public int? ACPD_COUNT { get; set; }

        public int? ACPD_REASON_ID { get; set; } //added
        public string ACPD_REASON { get; set; } //addded
        public int? ACPD_ADJUST { get; set; } //added
        [StringLength(100)]
        public string ACPD_ADJUST_BY { get; set; } //added
        [Column(TypeName = "datetime2")]
        public DateTime? ACPD_ADJUST_DATE { get; set; } //added
        public string ACPD_REMARK { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PLNTNRDDATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PLNTNRDDATE_D { get; set; } //added

        [Column(TypeName = "datetime2")]
        public DateTime? PLNACPDDATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PLNACPDDATE_D { get; set; }

        [StringLength(20)]
        public string DATA_GRP { get; set; }

        [StringLength(20)]
        public string DATA_SUBGRP { get; set; }

        public DateTime? LOADEDDATE { get; set; }

        [StringLength(5)]
        [Index("DN_DEPARTMENT", 4, IsUnique = false)]
        public string DEPARTMENT_ID { get; set; }

        [StringLength(100)]
        public string DEPARTMENT_Name { get; set; }

        //add 2/6/2016
        [StringLength(64)]
        public string LAST_SHPG_LOC_CD { get; set; }

        [StringLength(280)]
        public string LAST_SHPG_LOC_NAME { get; set; }

        [StringLength(30)]
        public string REGION_NAME_TH { get; set; }

        [StringLength(30)]
        public string REGION_NAME_EN { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LOADED_DATE { get; set; }
    }
}
