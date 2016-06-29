using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeAcceptMonthDb {
        private SCGLKPIDbContext db;
        public OntimeAcceptMonthDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<OntimeAcceptMonth> GetAll() {
            return db.OntimeAcceptMonths.ToList();
        }

        //GetById
        public OntimeAcceptMonth GetByID(int Id) {
            return db.OntimeAcceptMonths.Find(Id);
        }

        //Insert
        public void Insert(OntimeAcceptMonth ontimeAcceptMonth) {
            db.OntimeAcceptMonths.Add(ontimeAcceptMonth);
            Save();
        }

        //Update
        public void Update(OntimeAcceptMonth ontimeAcceptMonth) {
            db.Entry(ontimeAcceptMonth).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeAcceptMonth ontimeAcceptMonth = db.OntimeAcceptMonths.Find(Id);
            db.OntimeAcceptMonths.Remove(ontimeAcceptMonth);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
