using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class ReasonOutboundDb {
        private SCGLKPIDbContext db;
        public ReasonOutboundDb() {
            db = new SCGLKPIDbContext();
        }

        //GetAll
        public IEnumerable<ReasonOutbound> GetAll() {
            return db.ReasonOutbounds.ToList();
        }

        //GetById
        public ReasonOutbound GetByID(int Id) {
            return db.ReasonOutbounds.Find(Id);
        }

        //Insert
        public void Insert(ReasonOutbound reasonOutbound) {
            db.ReasonOutbounds.Add(reasonOutbound);
            Save();
        }

        //Update
        public void Update(ReasonOutbound reasonOutbound) {
            db.Entry(reasonOutbound).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            ReasonOutbound reasonOutbound = db.ReasonOutbounds.Find(Id);
            reasonOutbound.IsDeleted = true;
            db.Entry(reasonOutbound).State = EntityState.Modified;
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
