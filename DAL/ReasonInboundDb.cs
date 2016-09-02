using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;


namespace DAL {
    public class ReasonInboundDb {
        private SCGLKPIDbContext db;
        public ReasonInboundDb() {
            db = new SCGLKPIDbContext();
        }

        //GetAll
        public IEnumerable<ReasonInbound> GetAll() {
            return db.ReasonInbounds.ToList();
        }

        //GetById
        public ReasonInbound GetByID(int Id) {
            return db.ReasonInbounds.Find(Id);
        }

        //Insert
        public void Insert(ReasonInbound reasonInbound) {
            db.ReasonInbounds.Add(reasonInbound);
            Save();
        }

        //Update
        public void Update(ReasonInbound reasonInbound) {
            db.Entry(reasonInbound).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            ReasonInbound reasonInbound = db.ReasonInbounds.Find(Id);
            reasonInbound.IsDeleted = true;
            db.Entry(reasonInbound).State = EntityState.Modified;
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
