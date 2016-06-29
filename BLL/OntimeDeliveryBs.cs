using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OntimeDeliveryBs {
        private OntimeDeliveryDb objDb;
        public OntimeDeliveryBs() {
            objDb = new OntimeDeliveryDb();
        }

        //GetAll
        public IEnumerable<OntimeDelivery> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeDelivery GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeDelivery ontimeDelivery) {
            objDb.Insert(ontimeDelivery);
        }

        //Update
        public void Update(OntimeDelivery ontimeDelivery) {
            objDb.Update(ontimeDelivery);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
