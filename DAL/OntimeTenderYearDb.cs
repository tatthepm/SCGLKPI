using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;


namespace DAL {
    public class OntimeTenderYearDb {
        private SCGLKPIDbContext db;
        public OntimeTenderYearDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<OntimeTenderYear> GetAll() {
            return db.OntimeTenderYears.ToList();
        }

        //GetById
        public OntimeTenderYear GetByID(int Id) {
            return db.OntimeTenderYears.Find(Id);
        }

        //Insert
        public void Insert(OntimeTenderYear ontimeTenderYear) {
            db.OntimeTenderYears.Add(ontimeTenderYear);
            Save();
        }

        //Update
        public void Update(OntimeTenderYear ontimeTenderYear) {
            db.Entry(ontimeTenderYear).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeTenderYear ontimeTenderYear = db.OntimeTenderYears.Find(Id);
            db.OntimeTenderYears.Remove(ontimeTenderYear);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
