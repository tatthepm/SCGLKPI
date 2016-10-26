using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeAcceptYearDb {
        private SCGLKPIDbContext db;
        public OntimeAcceptYearDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<OntimeAcceptYear> GetAll() {
            return db.OntimeAcceptYears;
        }

        //GetById
        public OntimeAcceptYear GetByID(int Id) {
            return db.OntimeAcceptYears.Find(Id);
        }

        //Insert
        public void Insert(OntimeAcceptYear ontimeAcceptYear) {
            db.OntimeAcceptYears.Add(ontimeAcceptYear);
            Save();
        }

        //Update
        public void Update(OntimeAcceptYear ontimeAcceptYear) {
            db.Entry(ontimeAcceptYear).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeAcceptYear ontimeAcceptYear = db.OntimeAcceptYears.Find(Id);
            db.OntimeAcceptYears.Remove(ontimeAcceptYear);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
