using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class DWH_ONTIME_DNBs {
        private DWH_ONTIME_DNDb objDb;
        public DWH_ONTIME_DNBs() {
            objDb = new DWH_ONTIME_DNDb();
        }

        //GetAll
        public IEnumerable<DWH_ONTIME_DN> GetAll() {
            return objDb.GetAll();
        }
        //GetCount
        public int GetCount()
        {
            return objDb.GetCount();
        }

        //GetById
        public DWH_ONTIME_DN GetByID(string DELVNO) {
            return objDb.GetByID(DELVNO);
        }

        //Insert
        public void Insert(DWH_ONTIME_DN DWH_ONTIME_DN) {
            objDb.Insert(DWH_ONTIME_DN);
        }

        //Update
        public void Update(DWH_ONTIME_DN DWH_ONTIME_DN) {
            objDb.Update(DWH_ONTIME_DN);
        }

        //Delete
        public void Delete(string DELVNO) {
            objDb.Delete(DELVNO);
        }
    }
}
