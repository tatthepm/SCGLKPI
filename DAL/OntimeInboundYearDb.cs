using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeInboundYearDb {
        private SCGLKPIDbContext db;
        public OntimeInboundYearDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<OntimeInboundYear> GetAll() {
            return db.OntimeInboundYears.ToList();
        }

        //GetById
        public OntimeInboundYear GetByID(int Id) {
            return db.OntimeInboundYears.Find(Id);
        }

        //Insert
        public void Insert(OntimeInboundYear ontimeInboundYear) {
            db.OntimeInboundYears.Add(ontimeInboundYear);
            Save();
        }

        //Update
        public void Update(OntimeInboundYear ontimeInboundYear) {
            db.Entry(ontimeInboundYear).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeInboundYear ontimeInboundYear = db.OntimeInboundYears.Find(Id);
            db.OntimeInboundYears.Remove(ontimeInboundYear);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
