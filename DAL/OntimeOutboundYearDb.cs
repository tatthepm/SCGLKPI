using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;


namespace DAL {
    public class OntimeOutboundYearDb {
        private SCGLKPIDbContext db;
        public OntimeOutboundYearDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<OntimeOutboundYear> GetAll() {
            return db.OntimeOutboundYears.ToList();
        }

        //GetById
        public OntimeOutboundYear GetByID(int Id) {
            return db.OntimeOutboundYears.Find(Id);
        }

        //Insert
        public void Insert(OntimeOutboundYear ontimeOutboundYear) {
            db.OntimeOutboundYears.Add(ontimeOutboundYear);
            Save();
        }

        //Update
        public void Update(OntimeOutboundYear ontimeOutboundYear) {
            db.Entry(ontimeOutboundYear).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeOutboundYear ontimeOutboundYear = db.OntimeOutboundYears.Find(Id);
            db.OntimeOutboundYears.Remove(ontimeOutboundYear);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
