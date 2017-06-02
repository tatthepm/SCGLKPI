using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class ShippingPointDb {
        private SCGLKPIDbContext db;
        public ShippingPointDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<ShippingPoints> GetAll() {
            return db.ShippingPoints;
        }
        //GetByFilter
        public IQueryable<ShippingPoints> GetByFilter(string month, string year)
        {
            return db.ShippingPoints.Where(x => x.Year == year && x.Month == month);
        }
        //GetById
        public ShippingPoints GetByID(string shipmentNo) {
            return db.ShippingPoints.Find(shipmentNo);
        }

        //Insert
        public void Insert(ShippingPoints ShippingPoint) {
            db.ShippingPoints.Add(ShippingPoint);
            Save();
        }

        //Update
        public void Update(ShippingPoints ShippingPoint) {
            db.Entry(ShippingPoint).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string shipmentNo) {
            ShippingPoints ShippingPoint = db.ShippingPoints.Find(shipmentNo);
            db.ShippingPoints.Remove(ShippingPoint);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
