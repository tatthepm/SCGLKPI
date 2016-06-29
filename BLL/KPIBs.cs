using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;


namespace BLL {
    public class KPIBs {
        private KPIDb objDb;
        public KPIBs() {
            objDb = new KPIDb();
        }

        //GetAll
        public IEnumerable<KPI> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public KPI GetByID(string kpiId) {
            return objDb.GetByID(kpiId);
        }

        //Insert
        public void Insert(KPI kpi) {
            objDb.Insert(kpi);
        }

        //Update
        public void Update(KPI kpi) {
            objDb.Update(kpi);
        }

        //Delete
        public void Delete(string kpiId) {
            objDb.Delete(kpiId);
        }
    }
}
