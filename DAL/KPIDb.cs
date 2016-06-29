using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
   public class KPIDb {
        private SCGLKPIDbContext db;
        public KPIDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<KPI> GetAll() {
            return db.KPIs.ToList();
        }

        //GetById
        public KPI GetByID(string kpiId) {
            return db.KPIs.Find(kpiId);
        }

        //Insert
        public void Insert(KPI kpi) {
            db.KPIs.Add(kpi);
            Save();
        }

        //Update
        public void Update(KPI kpi) {
            db.Entry(kpi).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string kpiId) {
            KPI kpi = db.KPIs.Find(kpiId);
            db.KPIs.Remove(kpi);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
