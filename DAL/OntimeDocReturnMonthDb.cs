using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeDocReturnMonthDb {
        private SCGLKPIDbContext db;
        public OntimeDocReturnMonthDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<OntimeDocReturnMonth> GetAll() {
            return db.OntimeDocReturnMonths;
        }

        //GetById
        public OntimeDocReturnMonth GetByID(int Id) {
            return db.OntimeDocReturnMonths.Find(Id);
        }

        //Insert
        public void Insert(OntimeDocReturnMonth ontimeDocReturnMonth) {
            db.OntimeDocReturnMonths.Add(ontimeDocReturnMonth);
            Save();
        }

        //Update
        public void Update(OntimeDocReturnMonth ontimeDocReturnMonth) {
            db.Entry(ontimeDocReturnMonth).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeDocReturnMonth ontimeDocReturnMonth = db.OntimeDocReturnMonths.Find(Id);
            db.OntimeDocReturnMonths.Remove(ontimeDocReturnMonth);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
