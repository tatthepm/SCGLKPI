using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;


namespace DAL {
    public class OntimeDeliveryMonthDb {
        private SCGLKPIDbContext db;
        public OntimeDeliveryMonthDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<OntimeDeliveryMonth> GetAll() {
            return db.OntimeDeliveryMonths;
        }

        //GetById
        public OntimeDeliveryMonth GetByID(int Id) {
            return db.OntimeDeliveryMonths.Find(Id);
        }

        //Insert
        public void Insert(OntimeDeliveryMonth ontimeDeliveryMonth) {
            db.OntimeDeliveryMonths.Add(ontimeDeliveryMonth);
            Save();
        }

        //Update
        public void Update(OntimeDeliveryMonth ontimeDeliveryMonth) {
            db.Entry(ontimeDeliveryMonth).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeDeliveryMonth ontimeDeliveryMonth = db.OntimeDeliveryMonths.Find(Id);
            db.OntimeDeliveryMonths.Remove(ontimeDeliveryMonth);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
