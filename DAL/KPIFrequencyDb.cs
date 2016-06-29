using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class KPIFrequencyDb {
        private SCGLKPIDbContext db;
        public KPIFrequencyDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<KPIFrequency> GetAll() {
            return db.KPIFrequencies.ToList();
        }

        //GetById
        public KPIFrequency GetByID(string kpiFrequencyId) {
            return db.KPIFrequencies.Find(kpiFrequencyId);
        }

        //Insert
        public void Insert(KPIFrequency kpiFrequency) {
            db.KPIFrequencies.Add(kpiFrequency);
            Save();
        }

        //Update
        public void Update(KPIFrequency kpiFrequency) {
            db.Entry(kpiFrequency).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string kpiFrequencyId) {
            KPIFrequency kpiFrequency = db.KPIFrequencies.Find(kpiFrequencyId);
            db.KPIFrequencies.Remove(kpiFrequency);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
