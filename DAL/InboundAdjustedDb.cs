using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class InboundAdjustedDb {
        private SCGLKPIDbContext db;
        public InboundAdjustedDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<InboundAdjusted> GetAll() {
            return db.InboundAdjusted.ToList();
        }

        //GetById
        public InboundAdjusted GetByID(string deliveryNote ) {
            return db.InboundAdjusted.Find(deliveryNote);
        }

        //Insert
        public void Insert(InboundAdjusted inboundAdjusted) {
            db.InboundAdjusted.Add(inboundAdjusted);
            Save();
        }

        //Update
        public void Update(InboundAdjusted inboundAdjusted) {
            db.Entry(inboundAdjusted).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            InboundAdjusted inboundAdjusted = db.InboundAdjusted.Find(deliveryNote);
            db.InboundAdjusted.Remove(inboundAdjusted);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
