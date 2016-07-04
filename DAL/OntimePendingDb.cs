using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimePendingDb {
        private SCGLKPIDbContext db;
        public OntimePendingDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<OntimePending> GetAll() {
            return db.OntimePendings.ToList();
        }

        //GetById
        public OntimePending GetByID(string deliveryNote) {
            return db.OntimePendings.Find(deliveryNote);
        }

        //Insert
        public void Insert(OntimePending ontimePending) {
            db.OntimePendings.Add(ontimePending);
            Save();
        }

        //Update
        public void Update(OntimePending ontimePending) {
            db.Entry(ontimePending).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            OntimePending ontimePending = db.OntimePendings.Find(deliveryNote);
            db.OntimePendings.Remove(ontimePending);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
