using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class DocReturnAdjustedDb {
        private SCGLKPIDbContext db;
        public DocReturnAdjustedDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<DocReturnAdjusted> GetAll() {
            return db.DocReturnAdjusted.ToList();
        }

        //GetById
        public DocReturnAdjusted GetByID(string deliveryNote) {
            return db.DocReturnAdjusted.Find(deliveryNote);
        }

        //Insert
        public void Insert(DocReturnAdjusted outboundAdjusted) {
            db.DocReturnAdjusted.Add(outboundAdjusted);
            Save();
        }

        //Update
        public void Update(DocReturnAdjusted docReturnAdjusted) {
            db.Entry(docReturnAdjusted).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            DocReturnAdjusted docReturnAdjusted = db.DocReturnAdjusted.Find(deliveryNote);
            db.DocReturnAdjusted.Remove(docReturnAdjusted);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
