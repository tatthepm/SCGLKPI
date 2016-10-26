using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class ReasonAcceptedDb {
        private SCGLKPIDbContext db;
        public ReasonAcceptedDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<ReasonAccepted> GetAll() {
            return db.ReasonAccepteds;
        }

        //GetById
        public ReasonAccepted GetByID(int Id) {
            return db.ReasonAccepteds.Find(Id);
        }

        //Insert
        public void Insert(ReasonAccepted reasonAccepted) {
            db.ReasonAccepteds.Add(reasonAccepted);
            Save();
        }

        //Update
        public void Update(ReasonAccepted reasonAccepted) {
            db.Entry(reasonAccepted).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            ReasonAccepted reasonAccepted = db.ReasonAccepteds.Find(Id);
            reasonAccepted.IsDeleted = true;
            db.Entry(reasonAccepted).State = EntityState.Modified;
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
