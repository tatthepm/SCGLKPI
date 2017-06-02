using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class ShippingPointBs {
        private ShippingPointDb objDb;
        public ShippingPointBs() {
            objDb = new ShippingPointDb();
        }
        //GetAll
        public IQueryable<ShippingPoints> GetAll() {
            return objDb.GetAll();
        }
        //GetByFilter
        public IQueryable<ShippingPoints> GetByFilter(string month, string year)
        {
            return objDb.GetByFilter(month, year);
        }
        //GetById
        public ShippingPoints GetByID(string shipmentNo) {
            return objDb.GetByID(shipmentNo);
        }

        //Insert
        public void Insert(ShippingPoints ShippingPoint) {
            objDb.Insert(ShippingPoint);
        }

        //Update
        public void Update(ShippingPoints ShippingPoint) {
            objDb.Update(ShippingPoint);
        }

        //Delete
        public void Delete(string shipmentNo) {
            objDb.Delete(shipmentNo);
        }
    }
}
