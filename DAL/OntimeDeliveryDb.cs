using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeDeliveryDb {
        private SCGLKPIDbContext db;
        public OntimeDeliveryDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<OntimeDelivery> GetAll() {
            return db.OntimeDeliveries.ToList();
        }

        //GetById
        public OntimeDelivery GetByID(int Id) {
            return db.OntimeDeliveries.Find(Id);
        }

        //Insert
        public void Insert(OntimeDelivery ontimeDelivery) {
            db.OntimeDeliveries.Add(ontimeDelivery);
            Save();
        }

        //Update
        public void Update(OntimeDelivery ontimeDelivery) {
            db.Entry(ontimeDelivery).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeDelivery ontimeDelivery = db.OntimeDeliveries.Find(Id);
            db.OntimeDeliveries.Remove(ontimeDelivery);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
