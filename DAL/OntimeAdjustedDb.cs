using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeAdjustedDb {
        private SCGLKPIDbContext db;
        public OntimeAdjustedDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<OntimeAdjusted> GetAll() {
            return db.OntimeAdjusted.ToList();
        }

        //GetById
        public OntimeAdjusted GetByID(string deliveryNote) {
            return db.OntimeAdjusted.Find(deliveryNote);
        }

        //Insert
        public void Insert(OntimeAdjusted ontimeAdjusted) {
            db.OntimeAdjusted.Add(ontimeAdjusted);
            Save();
        }

        //Update
        public void Update(OntimeAdjusted ontimeAdjusted) {
            db.Entry(ontimeAdjusted).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            OntimeAdjusted ontimeAdjusted = db.OntimeAdjusted.Find(deliveryNote);
            db.OntimeAdjusted.Remove(ontimeAdjusted);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
