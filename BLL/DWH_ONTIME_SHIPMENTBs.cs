using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class DWH_ONTIME_SHIPMENTBs {
        private DWH_ONTIME_SHIPMENTDb objDb;
        public DWH_ONTIME_SHIPMENTBs() {
            objDb = new DWH_ONTIME_SHIPMENTDb();
        }

        //GetAll
        public IEnumerable<DWH_ONTIME_SHIPMENT> GetAll() {
            return objDb.GetAll();
        }
        //GetCount
        public int GetCount()
        {
            return objDb.GetCount();
        }
        //GetById
        public DWH_ONTIME_SHIPMENT GetByID(string SHPMNTNO) {
            return objDb.GetByID(SHPMNTNO);
        }

        //Insert
        public void Insert(DWH_ONTIME_SHIPMENT DWH_ONTIME_SHIPMENT) {
            objDb.Insert(DWH_ONTIME_SHIPMENT);
        }

        //Update
        public void Update(DWH_ONTIME_SHIPMENT DWH_ONTIME_SHIPMENT) {
            objDb.Update(DWH_ONTIME_SHIPMENT);
        }

        //Delete
        public void Delete(string SHPMNTNO) {
            objDb.Delete(SHPMNTNO);
        }
    }
}
