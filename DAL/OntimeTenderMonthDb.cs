using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeTenderMonthDb {
        private SCGLKPIDbContext db;
        public OntimeTenderMonthDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<OntimeTenderMonth> GetAll() {
            return db.OntimeTenderMonths;
        }

        //GetById
        public OntimeTenderMonth GetByID(int Id) {
            return db.OntimeTenderMonths.Find(Id);
        }

        //Insert
        public void Insert(OntimeTenderMonth ontimeTenderMonth) {
            db.OntimeTenderMonths.Add(ontimeTenderMonth);
            Save();
        }

        //Update
        public void Update(OntimeTenderMonth ontimeTenderMonth) {
            db.Entry(ontimeTenderMonth).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeTenderMonth ontimeTenderMonth = db.OntimeTenderMonths.Find(Id);
            db.OntimeTenderMonths.Remove(ontimeTenderMonth);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
