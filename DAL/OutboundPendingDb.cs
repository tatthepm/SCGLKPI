using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OutboundPendingDb {
        private SCGLKPIDbContext db;
        public OutboundPendingDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<OutboundPending> GetAll() {
            return db.OutboundPendings.ToList();
        }

        //GetById
        public OutboundPending GetByID(string deliveryNote) {
            return db.OutboundPendings.Find(deliveryNote);
        }

        //Insert
        public void Insert(OutboundPending outboundPending) {
            db.OutboundPendings.Add(outboundPending);
            Save();
        }

        //Update
        public void Update(OutboundPending outboundPending) {
            db.Entry(outboundPending).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            OutboundPending outboundPending = db.OutboundPendings.Find(deliveryNote);
            db.OutboundPendings.Remove(outboundPending);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
