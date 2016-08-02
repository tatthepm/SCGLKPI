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

        //GetByFilter
        public IEnumerable<HomeKPI> GetByFilter(string department_id, string section_id, string segment, int month, int year)
        {
            return db.HomeKPIs.Where(x => x.DepartmentId == department_id 
            && x.SectionId == section_id 
            && x.Segment == segment
            && x.Year == year 
            && x.LastMonth == month).Take(1000);
        }

        //GetLastMonth
        public IEnumerable<HomeKPI> GetLastMonth()
        {
            DateTime LastMonth = DateTime.Now.AddMonths(-1);
            int Year = LastMonth.Year;
            int Month = LastMonth.Month;
            return db.HomeKPIs.Where(x=> x.Year == Year && x.LastMonth == Month).ToList();
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
