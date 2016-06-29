using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeDocReturnYearDb {
        private SCGLKPIDbContext db;
        public OntimeDocReturnYearDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<OntimeDocReturnYear> GetAll() {
            return db.OntimeDocReturnYears.ToList();
        }

        //GetById
        public OntimeDocReturnYear GetByID(int Id) {
            return db.OntimeDocReturnYears.Find(Id);
        }

        //Insert
        public void Insert(OntimeDocReturnYear ontimeDocReturnYear) {
            db.OntimeDocReturnYears.Add(ontimeDocReturnYear);
            Save();
        }

        //Update
        public void Update(OntimeDocReturnYear ontimeDocReturnYear) {
            db.Entry(ontimeDocReturnYear).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeDocReturnYear ontimeDocReturnYear = db.OntimeDocReturnYears.Find(Id);
            db.OntimeDocReturnYears.Remove(ontimeDocReturnYear);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
