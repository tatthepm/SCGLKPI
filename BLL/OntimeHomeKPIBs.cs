using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class HomeKPIBs
    {
        private HomeKPIDb objDb;
        public HomeKPIBs() {
            objDb = new HomeKPIDb();
        }

        //GetAll
        public IEnumerable<HomeKPI> GetAll() {
            return objDb.GetAll();
        }

        //Insert
        public void Insert(HomeKPI homeKPI) {
            objDb.Insert(homeKPI);
        }

        //Update
        public void Update(HomeKPI homeKPI) {
            objDb.Update(homeKPI);
        }

        //Delete
        public void Delete(string month_year) {
            objDb.Delete(month_year);
        }
    }
}
