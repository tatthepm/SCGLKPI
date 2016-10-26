using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeOutboundMonthDb {
        private SCGLKPIDbContext db;
        public OntimeOutboundMonthDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<OntimeOutboundMonth> GetAll() {
            return db.OntimeOutboundMonths;
        }

        //GetById
        public OntimeOutboundMonth GetByID(int Id) {
            return db.OntimeOutboundMonths.Find(Id);
        }

        //Insert
        public void Insert(OntimeOutboundMonth ontimeOutboundMonth) {
            db.OntimeOutboundMonths.Add(ontimeOutboundMonth);
            Save();
        }

        //Update
        public void Update(OntimeOutboundMonth ontimeOutboundMonth) {
            db.Entry(ontimeOutboundMonth).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeOutboundMonth ontimeOutboundMonth = db.OntimeOutboundMonths.Find(Id);
            db.OntimeOutboundMonths.Remove(ontimeOutboundMonth);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
