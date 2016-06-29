using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OutboundDelayDb {
        private SCGLKPIDbContext db;
        public OutboundDelayDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<OutboundDelay> GetAll() {
            return db.OutboundDelays.ToList();
        }

        //GetById
        public OutboundDelay GetByID(string deliveryNote) {
            return db.OutboundDelays.Find(deliveryNote);
        }

        //Insert
        public void Insert(OutboundDelay outboundDelay) {
            db.OutboundDelays.Add(outboundDelay);
            Save();
        }

        //Update
        public void Update(OutboundDelay outboundDelay) {
            db.Entry(outboundDelay).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            OutboundDelay outboundDelay = db.OutboundDelays.Find(deliveryNote);
            db.OutboundDelays.Remove(outboundDelay);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
