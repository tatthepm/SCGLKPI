using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
   public class OntimeDeliveryMonthBs {
        private OntimeDeliveryMonthDb objDb;
        public OntimeDeliveryMonthBs() {
            objDb = new OntimeDeliveryMonthDb();
        }
        //GetAll
        public IEnumerable<OntimeDeliveryMonth> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeDeliveryMonth GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeDeliveryMonth ontimeDeliveryMonth) {
            objDb.Insert(ontimeDeliveryMonth);
        }

        //Update
        public void Update(OntimeDeliveryMonth ontimeDeliveryMonth) {
            objDb.Update(ontimeDeliveryMonth);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
