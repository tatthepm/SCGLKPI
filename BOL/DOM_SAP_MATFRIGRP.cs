using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
  public class DOM_SAP_MATFRIGRP {

        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(8)]
        public string MATFRIGRP { get; set; }

        [StringLength(100)]
        public string MATNAME { get; set; }

        [StringLength(3)]
        public string WORKTYPE { get; set; }

        [StringLength(3)]
        public string SUB_WORKTYPE { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LOADED_DATE { get; set; }
    }
}
