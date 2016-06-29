using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
    public class DOM_MDS_ORGANIZATION {

        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(8)]
        public string MATFRIGRP { get; set; }

        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(4)]
        public string SHPPOINT_ID { get; set; }

        [Key, Column(Order = 2), DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(4)]
        public string REGION_ID { get; set; }

        [Key, Column(Order = 3), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime VALIDFROM { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? VALIDTO { get; set; }

        [StringLength(150)]
        public string SHPPOINT_ID_Name { get; set; }

        [StringLength(100)]
        public string REGION_ID_Name { get; set; }

        [StringLength(10)]
        public string COMPCODE { get; set; }

        [StringLength(50)]
        public string COMPANY_NAME { get; set; }

        [StringLength(5)]
        public string DEPARTMENT_ID { get; set; }

        [StringLength(100)]
        public string DEPARTMENT_Name { get; set; }

        [StringLength(5)]
        public string SECTION_ID { get; set; }

        [StringLength(100)]
        public string SECTION_NAME { get; set; }

        [StringLength(100)]
        public string BU { get; set; }

        [StringLength(100)]
        public string TYPE { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? EnterDateTime { get; set; }

        [StringLength(100)]
        public string EnterUserName { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LastChgDateTime { get; set; }

        [StringLength(100)]
        public string LastChgUserName { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LOADED_DATE { get; set; }

    }
}
