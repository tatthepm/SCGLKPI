using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OutboundAdjustedDb {
        private SCGLKPIDbContext db;
        public OutboundAdjustedDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<OutboundAdjusted> GetAll() {
            return db.OutboundAdjusted.ToList();
        }

        //GetById
        public OutboundAdjusted GetByID(string deliveryNote) {
            return db.OutboundAdjusted.Find(deliveryNote);
        }

        //Insert
        public void Insert(OutboundAdjusted outboundAdjusted) {
            db.OutboundAdjusted.Add(outboundAdjusted);
            Save();
        }

        //Update
        public void Update(OutboundAdjusted outboundAdjusted) {
            db.Entry(outboundAdjusted).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            OutboundAdjusted outboundAdjusted = db.OutboundAdjusted.Find(deliveryNote);
            db.OutboundAdjusted.Remove(outboundAdjusted);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
