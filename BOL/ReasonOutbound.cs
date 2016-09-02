using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
    public class ReasonOutbound {
        public int Id { get; set; }

        [StringLength(255)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public bool IsAdjust { get; set; }
        public bool IsDeleted { get; set; }
    }
}
