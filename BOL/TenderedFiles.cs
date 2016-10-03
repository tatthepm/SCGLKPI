using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL
{
    public class TenderedFiles
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [StringLength(10)]
        [Index(IsUnique = false)]
        public string SHPMNTNO { get; set; }
        [StringLength(800)]
        public string FILEPATH { get; set; }
        [StringLength(800)]
        public string REMARK { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LOADED_DATE { get; set; }
        [StringLength(50)]
        public string LOADED_BY { get; set; }
    }
}
