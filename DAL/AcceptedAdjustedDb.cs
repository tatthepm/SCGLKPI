using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
   public class AcceptedAdjustedDb {
        private SCGLKPIDbContext db;
        public AcceptedAdjustedDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<AcceptedAdjusted> GetAll() {
            return db.AcceptedAdjusted.ToList();
        }

        //GetById
        public AcceptedAdjusted GetByID(string shipmentNo) {
            return db.AcceptedAdjusted.Find(shipmentNo);
        }

        //Insert
        public void Insert(AcceptedAdjusted acceptedAdjusted) {
            db.AcceptedAdjusted.Add(acceptedAdjusted);
            Save();
        }

        //Update
        public void Update(AcceptedAdjusted acceptedAdjusted) {
            db.Entry(acceptedAdjusted).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string shipmentNo) {
            AcceptedAdjusted acceptedAdjusted = db.AcceptedAdjusted.Find(shipmentNo);
            db.AcceptedAdjusted.Remove(acceptedAdjusted);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
