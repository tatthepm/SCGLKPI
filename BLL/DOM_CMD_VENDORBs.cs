using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class DOM_CMD_VENDORBs {
        private DOM_CMD_VENDORDb objDb;
        public DOM_CMD_VENDORBs() {
            objDb = new DOM_CMD_VENDORDb();
        }

        //GetAll
        public IEnumerable<DOM_CMD_VENDOR> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public DOM_CMD_VENDOR GetByID(string VENDOR_CODE) {
            return objDb.GetByID(VENDOR_CODE);
        }

        //Insert
        public void Insert(DOM_CMD_VENDOR DOM_CMD_VENDOR) {
            objDb.Insert(DOM_CMD_VENDOR);
        }

        //Update
        public void Update(DOM_CMD_VENDOR DOM_CMD_VENDOR) {
            objDb.Update(DOM_CMD_VENDOR);
        }

        //Delete
        public void Delete(string VENDOR_CODE) {
            objDb.Delete(VENDOR_CODE);
        }
    }
}
