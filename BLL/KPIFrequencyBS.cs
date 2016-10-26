using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;


namespace BLL {
   public class KPIFrequencyBS {
        private KPIFrequencyDb objDb;
        public KPIFrequencyBS() {
            objDb = new KPIFrequencyDb();
        }

        //GetAll
        public IQueryable<KPIFrequency> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public KPIFrequency GetByID(string kpiFrequencyId) {
            return objDb.GetByID(kpiFrequencyId);
        }

        //Insert
        public void Insert(KPIFrequency kpiFrequency) {
            objDb.Insert(kpiFrequency);
        }

        //Update
        public void Update(KPIFrequency kpiFrequency) {
            objDb.Update(kpiFrequency);
        }

        //Delete
        public void Delete(string kpiFrequencyId) {
            objDb.Delete(kpiFrequencyId);
        }
    }
}
