using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
    public class DWH_ONTIME_DN {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10)]
        public string DELVNO { get; set; }

        [StringLength(10)]
        public string SALEORDNO { get; set; }

        [StringLength(10)]
        public string SHPMNTNO { get; set; }

        [StringLength(3)]
        public string WORK_TYPE { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ACTGIDATE { get; set; }


        [Column(TypeName = "datetime2")]
        public DateTime? SHCRDATE { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? SHUPDATE { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? TNRDDATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ACPDDATE { get; set; }

        [StringLength(5)]
        public string FLEET_ID { get; set; }

        [StringLength(3)]
        public string SEGMENT_ID { get; set; }

        [StringLength(8)]
        public string MATFRIGRP { get; set; }

        public string MATNAME { get; set; }

        [StringLength(4)]
        public string SHPPOINT { get; set; }

        [StringLength(4)]
        public string REGION_ID { get; set; }

        [StringLength(4)]
        public string REGION_ID_SAP { get; set; }

        [StringLength(32)]
        public string CARRIER_ID { get; set; }

        [StringLength(10)]
        public string SOLDTO { get; set; }
        [StringLength(800)]
        public string SOLDTO_NAME { get; set; }

        [StringLength(5)]
        public string SECTION_ID { get; set; }

        [StringLength(100)]
        public string SECTION_NAME { get; set; }

        [StringLength(10)]
        public string SHIPTO { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CRDLVDATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PLDLVDATE { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ACDLVDATE { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? PICKDATE { get; set; }

        public double? DISTANCE { get; set; }

        [StringLength(3)]
        public string INCOTERM { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ORDCMPDATE { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? REQUESTED_DATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ENDSHPDATE { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? DOCRETDATE_SCGL { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DOCRETDATE_CUST { get; set; }

        public int? INB_ONTIME_FLAG { get; set; }

        public int? INB_COUNT { get; set; }

        public int? INB_REASON_ID { get; set; } //added
        public string INB_REASON { get; set; } //added
        public int? INB_ADJUST { get; set; }    //added 
        [StringLength(100)]
        public string INB_ADJUST_BY { get; set; } //added
        [Column(TypeName = "datetime2")]
        public DateTime? INB_ADJUST_DATE { get; set; } //added
        public string INB_REMARK { get; set; } //added


        public int? OUTB_ONTIME_FLAG { get; set; }

        public int? OUTB_COUNT { get; set; }

        public int? OUTB_REASON_ID { get; set; } //added
        public string OUTB_REASON { get; set; } //added
        public int? OUTB_ADJUST { get; set; } //added
        [StringLength(100)]
        public string OUTB_ADJUST_BY { get; set; } //added
        [Column(TypeName = "datetime2")]
        public DateTime? OUTB_ADJUST_DATE { get; set; } //added
        public string OUTB_REMARK { get; set; } //added

        public int? ON_TIME_FLAG { get; set; }

        public int? ON_TIME_COUNT { get; set; }

        public int? ON_TIME_REASON_ID { get; set; } //added
        public string ON_TIME_REASON { get; set; } //added
        public int? ON_TIME_ADJUST { get; set; } //added
        [StringLength(100)]
        public string ON_TIME_ADJUST_BY { get; set; } //added
        [Column(TypeName = "datetime2")]
        public DateTime? ON_TIME_ADJUST_DATE { get; set; } //added
        public string ON_TIME_REMARK { get; set; } //added


        public int? SCGL_DOCRET_ONTIME_FLAG { get; set; }

        public int? SCGL_DOCRET_COUNT { get; set; }

        public int? SCGL_DOCRET_REASON_ID { get; set; } //added
        public string SCGL_DOCRET_REASON { get; set; } //added
        public int? SCGL_DOCRET_ADJUST { get; set; } //added
        [StringLength(100)]
        public string SCGL_DOCRET_ADJUST_BY { get; set; } //added
        [Column(TypeName = "datetime2")]
        public DateTime? SCGL_DOCRET_ADJUST_DATE { get; set; } //added
        public string SCGL_DOCRET_REMARK { get; set; } //added

        public int? CUST_DOCRET_ONTIME_FLAG { get; set; }

        public int? CUST_DOCRET_COUNT { get; set; }

        public int? CUST_DOCRET_REASON_ID { get; set; } //added
        public string CUST_DOCRET_REASON { get; set; } //added
        public int? CUST_DOCRET_ADJUST { get; set; } //added
        [StringLength(100)]
        public string CUST_DOCRET_ADJUST_BY { get; set; } //added
        [Column(TypeName = "datetime2")]
        public DateTime? CUST_DOCRET_ADJUST_DATE { get; set; } //added
        public string CUST_DOCRET_REMARK { get; set; } //added 

        [StringLength(20)]
        public string TRUCK_TYPE { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? PLNINBDATE { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? PLNOUTBDATE { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? PLNONTIMEDATE { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? PLNONTIMEDATE_WINDOW { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? PLNDOCRETDATE_SCGL { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? PLNDOCRETDATE_CUST { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? INB_ORI_ONTIME { get; set; }

        public int? INB_ORI_ONTIME_REASON_ID { get; set; } //added
        public string INB_ORI_ONTIME_REASON { get; set; } //added
        public int? INB_ORI_ONTIME_ADJUST { get; set; } //added
        [StringLength(100)]
        public string INB_ORI_ONTIME_ADJUST_BY { get; set; } //added
        [Column(TypeName = "datetime2")]
        public DateTime? INB_ORI_ONTIME_ADJUST_DATE { get; set; } //added
        public string INB_ORI_ONTIME_REMARK { get; set; } //added


        [Column(TypeName = "datetime2")]
        public DateTime? INB_DES_ONTIME { get; set; }

        public int? INB_DES_ONTIME_REASON_ID { get; set; } //added
        public string INB_DES_ONTIME_REASON { get; set; } //added
        public int? INB_DES_ONTIME_ADJUST { get; set; } //added
        [StringLength(100)]
        public string INB_DES_ONTIME_ADJUST_BY { get; set; } //added
        [Column(TypeName = "datetime2")]
        public DateTime? INB_DES_ONTIME_ADJUST_DATE { get; set; } //added
        public string INB_DES_ONTIME_REMARK { get; set; } //added


        [Column(TypeName = "datetime2")]
        public DateTime? OUTB_ORI_ONTIME { get; set; }

        public int? OUTB_ORI_ONTIME_REASON_ID { get; set; } //added
        public string OUTB_ORI_ONTIME_REASON { get; set; } //added
        public int? OUTB_ORI_ONTIME_ADJUST { get; set; } //added
        [StringLength(100)]
        public string OUTB_ORI_ONTIME_ADJUST_BY { get; set; } //added
        [Column(TypeName = "datetime2")]
        public DateTime? OUTB_ORI_ONTIME_ADJUST_DATE { get; set; } //added
        public string OUTB_ORI_ONTIME_REMARK { get; set; } //added


        [Column(TypeName = "datetime2")]
        public DateTime? OUTB_DES_ONTIME { get; set; }

        public int? OUTB_DES_ONTIME_REASON_ID { get; set; } //added
        public string OUTB_DES_ONTIME_REASON { get; set; } //added
        public int? OUTB_DES_ONTIME_ADJUST { get; set; } //added
        [StringLength(100)]
        public string OUTB_DES_ONTIME_ADJUST_BY { get; set; } //added
        [Column(TypeName = "datetime2")]
        public DateTime? OUTB_DES_ONTIME_ADJUST_DATE { get; set; } //added
        public string OUTB_DES_ONTIME_REMARK { get; set; }

        [StringLength(20)]
        public string DATA_GRP { get; set; }

        [StringLength(20)]
        public string DATA_SUBGRP { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LOADEDDATE { get; set; }

        //added date 20 fields

        [Column(TypeName = "datetime2")]
        public DateTime? ACTGIDATE_D { get; set; }  //1

        [Column(TypeName = "datetime2")]
        public DateTime? TNRDDATE_D { get; set; }  //2

        [Column(TypeName = "datetime2")]
        public DateTime? ACPDDATE_D { get; set; }  //3

        [Column(TypeName = "datetime2")]
        public DateTime? CRDLVDATE_D { get; set; } //4

        [Column(TypeName = "datetime2")]
        public DateTime? REQUESTED_DATE_D { get; set; } //5

        [Column(TypeName = "datetime2")]
        public DateTime? SHCRDATE_D { get; set; } //6

        [Column(TypeName = "datetime2")]
        public DateTime? SHUPDATE_D { get; set; } //7

        [Column(TypeName = "datetime2")]
        public DateTime? PLDLVDATE_D { get; set; } //8

        [Column(TypeName = "datetime2")]
        public DateTime? ACDLVDATE_D { get; set; } //9

        [Column(TypeName = "datetime2")]
        public DateTime? PICKDATE_D { get; set; } //10

        [Column(TypeName = "datetime2")]
        public DateTime? ORDCMPDATE_D { get; set; } //11

        [Column(TypeName = "datetime2")]
        public DateTime? ENDSHPDATE_D { get; set; } //12

        [Column(TypeName = "datetime2")]
        public DateTime? DOCRETDATE_SCGL_D { get; set; } //13

        [Column(TypeName = "datetime2")]
        public DateTime? DOCRETDATE_CUST_D { get; set; } //14

        [Column(TypeName = "datetime2")]
        public DateTime? PLNINBDATE_D { get; set; }  //15

        [Column(TypeName = "datetime2")]
        public DateTime? PLNOUTBDATE_D { get; set; }  //16

        [Column(TypeName = "datetime2")]
        public DateTime? PLNONTIMEDATE_D { get; set; }  //17

        [Column(TypeName = "datetime2")]
        public DateTime? PLNONTIMEDATE_WINDOW_D { get; set; } //18

        [Column(TypeName = "datetime2")]
        public DateTime? PLNDOCRETDATE_SCGL_D { get; set; } //19

        [Column(TypeName = "datetime2")]
        public DateTime? PLNDOCRETDATE_CUST_D { get; set; } //20

        [StringLength(5)]
        public string DEPARTMENT_ID { get; set; }

        [StringLength(100)]
        public string DEPARTMENT_Name { get; set; }

        //add 2/6/2016
        [StringLength(64)]
        public string TO_SHPG_LOC_CD { get; set; } //64

        [StringLength(280)]
        public string TO_SHPG_LOC_NAME { get; set; } //280

        [StringLength(30)]
        public string REGION_NAME_TH { get; set; } //30

        [StringLength(30)]
        public string REGION_NAME_EN { get; set; }//30

        [Column(TypeName = "datetime2")]
        public DateTime? LOADED_DATE { get; set; }
    }
}
