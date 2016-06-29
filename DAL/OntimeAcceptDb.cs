using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
   public class OntimeAcceptDb {
        private SCGLKPIDbContext db;
        public OntimeAcceptDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<OntimeAccept> GetAll() {
            return db.OntimeAccepts.ToList();
        }

        //GetById
        public OntimeAccept GetByID(int Id) {
            return db.OntimeAccepts.Find(Id);
        }

        //Insert
        public void Insert(OntimeAccept ontimeAccept) {
            db.OntimeAccepts.Add(ontimeAccept);
            Save();
        }

        //Update
        public void Update(OntimeAccept ontimeAccept) {
            db.Entry(ontimeAccept).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeAccept ontimeAccept = db.OntimeAccepts.Find(Id);
            db.OntimeAccepts.Remove(ontimeAccept);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
