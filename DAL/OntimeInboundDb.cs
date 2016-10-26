using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeInboundDb {
        private SCGLKPIDbContext db;
        public OntimeInboundDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<OntimeInbound> GetAll() {
            return db.OntimeInbounds;
        }

        //GetById
        public OntimeInbound GetByID(int Id) {
            return db.OntimeInbounds.Find(Id);
        }

        //Insert
        public void Insert(OntimeInbound ontimeInbound) {
            db.OntimeInbounds.Add(ontimeInbound);
            Save();
        }

        //Update
        public void Update(OntimeInbound ontimeInbound) {
            db.Entry(ontimeInbound).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeInbound ontimeInbound = db.OntimeInbounds.Find(Id);
            db.OntimeInbounds.Remove(ontimeInbound);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
