using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL
{
    public class SaleSummaryDaily
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Index(IsUnique = false)]
        [Column(TypeName = "datetime2")]

        public DateTime ActualGiDate { get; set; }
        [Index(IsUnique = false)]
        [Column(TypeName = "datetime2")]
        public DateTime LoadedDate { get; set; }

        [StringLength(5)]
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }

        [StringLength(5)]
        public string SegmentId { get; set; }
        public string SegmentName { get; set; }

        public string MatName { get; set; }
        public string MatFriGrp { get; set; }

        public int SumOfTender { get; set; }
        public int OnTimeTender { get; set; }
        public int AdjustedTender { get; set; }

        public int SumOfAccept { get; set; }
        public int OnTimeAccept { get; set; }
        public int AdjustedAccept { get; set; }

        public int SumOfInbound { get; set; }
        public int OnTimeInbound { get; set; }
        public int AdjustedInbound { get; set; }

        public int SumOfOutbound { get; set; }
        public int OnTimeOutbound { get; set; }
        public int AdjustedOutbound { get; set; }

        public int SumOfDelivery { get; set; }
        public int OnTimeDelivery { get; set; }
        public int AdjustedDelivery { get; set; }

        public int SumOfDocreturn { get; set; }
        public int OnTimeDocreturn { get; set; }
        public int AdjustedDocreturn { get; set; }


    }
}
