using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;


namespace DAL {
    public class OntimeInboundMonthDb {
        private SCGLKPIDbContext db;
        public OntimeInboundMonthDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<OntimeInboundMonth> GetAll() {
            return db.OntimeInboundMonths.ToList();
        }

        //GetById
        public OntimeInboundMonth GetByID(int Id) {
            return db.OntimeInboundMonths.Find(Id);
        }

        //Insert
        public void Insert(OntimeInboundMonth ontimeInboundMonth) {
            db.OntimeInboundMonths.Add(ontimeInboundMonth);
            Save();
        }

        //Update
        public void Update(OntimeInboundMonth ontimeInboundMonth) {
            db.Entry(ontimeInboundMonth).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeInboundMonth ontimeInboundMonth = db.OntimeInboundMonths.Find(Id);
            db.OntimeInboundMonths.Remove(ontimeInboundMonth);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
