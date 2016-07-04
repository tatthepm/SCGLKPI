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
        public IEnumerable<AcceptedPending> GetAll() {
            return db.AcceptedPendings.ToList();
        }

        //GetById
        public AcceptedPending GetByID(string shipmentNo) {
            return db.AcceptedPendings.Find(shipmentNo);
        }

        //Insert
        public void Insert(AcceptedPending acceptedPending) {
            db.AcceptedPendings.Add(acceptedPending);
            Save();
        }

        //Update
        public void Update(AcceptedPending acceptedPending) {
            db.Entry(acceptedPending).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string shipmentNo) {
            AcceptedPending acceptedPending = db.AcceptedPendings.Find(shipmentNo);
            db.AcceptedPendings.Remove(acceptedPending);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
