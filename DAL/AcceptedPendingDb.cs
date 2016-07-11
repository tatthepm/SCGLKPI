using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
   public class AcceptedPendingDb {
        private SCGLKPIDbContext db;
        public AcceptedPendingDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<AcceptPending> GetAll() {
            return db.AcceptPendings.ToList();
        }

        //GetById
        public AcceptPending GetByID(string shipmentNo) {
            return db.AcceptPendings.Find(shipmentNo);
        }

        //Insert
        public void Insert(AcceptPending acceptedPending) {
            db.AcceptPendings.Add(acceptedPending);
            Save();
        }

        //Update
        public void Update(AcceptPending acceptedPending) {
            db.Entry(acceptedPending).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string shipmentNo) {
            AcceptPending acceptedPending = db.AcceptPendings.Find(shipmentNo);
            db.AcceptPendings.Remove(acceptedPending);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
