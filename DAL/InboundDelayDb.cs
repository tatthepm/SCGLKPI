using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class InboundDelayDb {
        private SCGLKPIDbContext db;
        public InboundDelayDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<InboundDelay> GetAll() {
            return db.InboundDelays.ToList();
        }

        //GetById
        public InboundDelay GetByID(string deliveryNote ) {
            return db.InboundDelays.Find(deliveryNote);
        }

        //Insert
        public void Insert(InboundDelay inboundDelay) {
            db.InboundDelays.Add(inboundDelay);
            Save();
        }

        //Update
        public void Update(InboundDelay inboundDelay) {
            db.Entry(inboundDelay).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            InboundDelay inboundDelay = db.InboundDelays.Find(deliveryNote);
            db.InboundDelays.Remove(inboundDelay);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
