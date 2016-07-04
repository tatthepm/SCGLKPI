using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
   public class AcceptedDelayDb {
        private SCGLKPIDbContext db;
        public AcceptedDelayDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<AcceptedDelay> GetAll() {
            return db.AcceptedDelays.ToList();
        }

        //GetById
        public AcceptedDelay GetByID(string shipmentNo) {
            return db.AcceptedDelays.Find(shipmentNo);
        }

        //Insert
        public void Insert(AcceptedDelay acceptedDelay) {
            db.AcceptedDelays.Add(acceptedDelay);
            Save();
        }

        //Update
        public void Update(AcceptedDelay acceptedDelay) {
            db.Entry(acceptedDelay).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string shipmentNo) {
            AcceptedDelay acceptedDelay = db.AcceptedDelays.Find(shipmentNo);
            db.AcceptedDelays.Remove(acceptedDelay);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
