using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeDeliveryYearDb {
        private SCGLKPIDbContext db;
        public OntimeDeliveryYearDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<OntimeDeliveryYear> GetAll() {
            return db.OntimeDeliveryYears;
        }

        //GetById
        public OntimeDeliveryYear GetByID(int Id) {
            return db.OntimeDeliveryYears.Find(Id);
        }

        //Insert
        public void Insert(OntimeDeliveryYear ontimeDeliveryYear) {
            db.OntimeDeliveryYears.Add(ontimeDeliveryYear);
            Save();
        }

        //Update
        public void Update(OntimeDeliveryYear ontimeDeliveryYear) {
            db.Entry(ontimeDeliveryYear).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeDeliveryYear ontimeDeliveryYear = db.OntimeDeliveryYears.Find(Id);
            db.OntimeDeliveryYears.Remove(ontimeDeliveryYear);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
