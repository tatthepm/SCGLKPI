using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class HomeKPIDb {
        private SCGLKPIDbContext db;
        public HomeKPIDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<HomeKPI> GetAll() {
            return db.HomeKPIs.ToList();
        }

        //Insert
        public void Insert(HomeKPI HomeKPI) {
            db.HomeKPIs.Add(HomeKPI);
            Save();
        }

        //Update
        public void Update(HomeKPI HomeKPI) {
            db.Entry(HomeKPI).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string year_month) {
            HomeKPI HomeKPI = db.HomeKPIs.Find(year_month);
            db.HomeKPIs.Remove(HomeKPI);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
