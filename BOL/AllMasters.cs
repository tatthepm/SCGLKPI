using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL {
    public class TenderUsers
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [StringLength(4)]
        public string Year { get; set; }
        [StringLength(2)]
        public string Month { get; set; }
        [StringLength(40)]
        [Index(IsUnique = false)]
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class ShippingPoints
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [StringLength(4)]
        public string Year { get; set; }
        [StringLength(2)]
        public string Month { get; set; }
        [StringLength(15)]
        [Index(IsUnique = false)]
        public string ShippingPointsId { get; set; }
        public string ShippingPointsName { get; set; }
        [StringLength(15)]
        [Index(IsUnique = false)]
        public string ShipToId { get; set; }
        public string ShipToName { get; set; }
        [StringLength(15)]
        [Index(IsUnique = false)]
        public string SoldToId { get; set; }
        public string SoldToName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class Carriers
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [StringLength(4)]
        public string Year { get; set; }
        [StringLength(2)]
        public string Month { get; set; }
        [StringLength(15)]
        [Index(IsUnique = false)]
        public string CarrierId { get; set; }
        public string CarrierName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class MatFreightGroups
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [StringLength(4)]
        public string Year { get; set; }
        [StringLength(2)]
        public string Month { get; set; }
        [StringLength(10)]
        [Index(IsUnique = false)]
        public string MatFriGrp { get; set; }
        public string MatName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
