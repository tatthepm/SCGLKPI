using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;


namespace BLL {
    public class HomeKPIBs {
        private HomeKPIDb objDb;
        public HomeKPIBs() {
            objDb = new HomeKPIDb();
        }

        //GetAll
        public IEnumerable<HomeKPI> GetAll() {
            return objDb.GetAll();
        }

        //GetByFilter
        public IEnumerable<HomeKPI> GetByFilter(string department_id, string section_id, string segment, int month, int year)
        {
            return objDb.GetByFilter(department_id, section_id, segment, month, year);
        }

        //GetLastMonth
        public IEnumerable<HomeKPI> GetLastMonth() {
            return objDb.GetLastMonth();
        }

        //GetMonth
        public IEnumerable<HomeKPI> GetMonth(int monthAdjust)
        {
            return objDb.GetMonth(monthAdjust);
        }

        //Insert
        public void Insert(HomeKPI kpi) {
            objDb.Insert(kpi);
        }

        //Update
        public void Update(HomeKPI kpi) {
            objDb.Update(kpi);
        }

        //Delete
        public void Delete(string kpiId) {
            objDb.Delete(kpiId);
        }
    }
}
