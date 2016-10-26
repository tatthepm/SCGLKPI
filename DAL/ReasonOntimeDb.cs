using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class ReasonOntimeDb {
        private SCGLKPIDbContext db;
        public ReasonOntimeDb() {
            db = new SCGLKPIDbContext();
        }

        //GetAll
        public IQueryable<ReasonOntime> GetAll() {
            return db.ReasonOntimes;
        }

        //GetById
        public ReasonOntime GetByID(int Id) {
            return db.ReasonOntimes.Find(Id);
        }

        //Insert
        public void Insert(ReasonOntime reasonOntime) {
            db.ReasonOntimes.Add(reasonOntime);
            Save();
        }

        //Update
        public void Update(ReasonOntime reasonOntime) {
            db.Entry(reasonOntime).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            ReasonOntime reasonOntime = db.ReasonOntimes.Find(Id);
            reasonOntime.IsDeleted = true;
            db.Entry(reasonOntime).State = EntityState.Modified;
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
