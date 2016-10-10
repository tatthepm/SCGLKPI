using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class ReasonTenderedDb {
        private SCGLKPIDbContext db;
        public ReasonTenderedDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<ReasonTendered> GetAll() {
            return db.ReasonTendereds.ToList();
        }

        //GetById
        public ReasonTendered GetByID(int Id) {
            return db.ReasonTendereds.Find(Id);
        }

        //Insert
        public void Insert(ReasonTendered ReasonTendereds) {
            db.ReasonTendereds.Add(ReasonTendereds);
            Save();
        }

        //Update
        public void Update(ReasonTendered ReasonTendereds) {
            db.Entry(ReasonTendereds).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            ReasonTendered ReasonTendereds = db.ReasonTendereds.Find(Id);
            ReasonTendereds.IsDeleted = true;
            db.Entry(ReasonTendereds).State = EntityState.Modified;
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
