using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeOutboundDb {
        private SCGLKPIDbContext db;
        public OntimeOutboundDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<OntimeOutbound> GetAll() {
            return db.OntimeOutbounds.ToList();
        }

        //GetById
        public OntimeOutbound GetByID(int Id) {
            return db.OntimeOutbounds.Find(Id);
        }

        //Insert
        public void Insert(OntimeOutbound ontimeOutbound) {
            db.OntimeOutbounds.Add(ontimeOutbound);
            Save();
        }

        //Update
        public void Update(OntimeOutbound ontimeOutbound) {
            db.Entry(ontimeOutbound).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeOutbound ontimeOutbound = db.OntimeOutbounds.Find(Id);
            db.OntimeOutbounds.Remove(ontimeOutbound);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
