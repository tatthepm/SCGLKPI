using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class InboundPendingDb {
        private SCGLKPIDbContext db;
        public InboundPendingDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<InboundPending> GetAll() {
            return db.InboundPendings.ToList();
        }

        //GetById
        public InboundPending GetByID(string deliveryNote ) {
            return db.InboundPendings.Find(deliveryNote);
        }

        //Insert
        public void Insert(InboundPending inboundPending) {
            db.InboundPendings.Add(inboundPending);
            Save();
        }

        //Update
        public void Update(InboundPending inboundPending) {
            db.Entry(inboundPending).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            InboundPending inboundPending = db.InboundPendings.Find(deliveryNote);
            db.InboundPendings.Remove(inboundPending);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
