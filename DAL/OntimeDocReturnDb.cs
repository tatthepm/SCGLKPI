using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;


namespace DAL {
    public class OntimeDocReturnDb {
        private SCGLKPIDbContext db;
        public OntimeDocReturnDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<OntimeDocReturn> GetAll() {
            return db.OntimeDocReturns.ToList();
        }

        //GetById
        public OntimeDocReturn GetByID(int Id) {
            return db.OntimeDocReturns.Find(Id);
        }

        //Insert
        public void Insert(OntimeDocReturn ontimeDocReturn) {
            db.OntimeDocReturns.Add(ontimeDocReturn);
            Save();
        }

        //Update
        public void Update(OntimeDocReturn ontimeDocReturn) {
            db.Entry(ontimeDocReturn).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeDocReturn ontimeDocReturn = db.OntimeDocReturns.Find(Id);
            db.OntimeDocReturns.Remove(ontimeDocReturn);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
