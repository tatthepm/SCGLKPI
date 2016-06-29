using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeDelayDb {
        private SCGLKPIDbContext db;
        public OntimeDelayDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<OntimeDelay> GetAll() {
            return db.OntimeDelays.ToList();
        }

        //GetById
        public OntimeDelay GetByID(string deliveryNote) {
            return db.OntimeDelays.Find(deliveryNote);
        }

        //Insert
        public void Insert(OntimeDelay ontimeDelay) {
            db.OntimeDelays.Add(ontimeDelay);
            Save();
        }

        //Update
        public void Update(OntimeDelay ontimeDelay) {
            db.Entry(ontimeDelay).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            OntimeDelay ontimeDelay = db.OntimeDelays.Find(deliveryNote);
            db.OntimeDelays.Remove(ontimeDelay);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
