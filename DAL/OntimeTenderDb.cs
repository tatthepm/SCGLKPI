using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeTenderDb {
        private SCGLKPIDbContext db;
        public OntimeTenderDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<OntimeTender> GetAll() {
            return db.OntimeTenders;
        }

        //GetById
        public OntimeTender GetByID(int Id) {
            return db.OntimeTenders.Find(Id);
        }

        //Insert
        public void Insert(OntimeTender ontimeTender) {
            db.OntimeTenders.Add(ontimeTender);
            Save();
        }

        //Update
        public void Update(OntimeTender ontimeTender) {
            db.Entry(ontimeTender).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeTender ontimeTender = db.OntimeTenders.Find(Id);
            db.OntimeTenders.Remove(ontimeTender);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
