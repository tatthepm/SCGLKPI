using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
    public class DOM_CMD_VENDOR {

        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(40)]
        public string VENDOR_CODE { get; set; }

        [StringLength(140)]
        public string VENDOR_NAME { get; set; }

        [StringLength(200)]
        public string STREET { get; set; }

        [StringLength(200)]
        public string DISTRICT { get; set; }

        [StringLength(140)]
        public string CITY { get; set; }

        [StringLength(12)]
        public string COUNTRY { get; set; }

        [StringLength(40)]
        public string POSTAL_CODE { get; set; }

        [StringLength(64)]
        public string TAX_NUMBER { get; set; }

        [StringLength(64)]
        public string TELEPHONE { get; set; }

        [StringLength(124)]
        public string FAX { get; set; }

        [StringLength(4)]
        public string COMPANY_CODE { get; set; }

        [StringLength(1)]
        public string FLAG_DEL { get; set; }

        [StringLength(20)]
        public string CREATE_BY { get; set; }
        [Column(TypeName ="datetime2")]
        public DateTime? CREATE_DATE { get; set; }

        [StringLength(20)]
        public string LASTUPDATE_BY { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LASTUPDATE_DATE { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LOADED_DATE { get; set; }

    }
}
