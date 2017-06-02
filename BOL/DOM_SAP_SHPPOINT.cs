using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL
{
    public class DOM_SAP_SHPPOINT
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(4)]
        public string SHIPPOINT { get; set; }

        [StringLength(30)]
        public string DESP { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LOADED_DATE { get; set; }
    }
}
