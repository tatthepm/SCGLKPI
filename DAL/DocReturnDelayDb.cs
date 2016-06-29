using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class DocReturnDelayDb {
        private SCGLKPIDbContext db;
        public DocReturnDelayDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<DocReturnDelay> GetAll() {
            return db.DocReturnDelays.ToList();
        }

        //GetById
        public DocReturnDelay GetByID(string deliveryNote) {
            return db.DocReturnDelays.Find(deliveryNote);
        }

        //Insert
        public void Insert(DocReturnDelay outboundDelay) {
            db.DocReturnDelays.Add(outboundDelay);
            Save();
        }

        //Update
        public void Update(DocReturnDelay docReturnDelay) {
            db.Entry(docReturnDelay).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            DocReturnDelay docReturnDelay = db.DocReturnDelays.Find(deliveryNote);
            db.DocReturnDelays.Remove(docReturnDelay);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
