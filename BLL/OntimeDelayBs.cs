using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;


namespace BLL {
    public class OntimeDelayBs {
        private OntimeDelayDb objDb;
        public OntimeDelayBs() {
            objDb = new OntimeDelayDb();
        }
        //GetAll
        public IEnumerable<OntimeDelay> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeDelay GetByID(string deliveryNote) {
            return objDb.GetByID(deliveryNote);
        }

        //Insert
        public void Insert(OntimeDelay ontimeDelay) {
            objDb.Insert(ontimeDelay);
        }

        //Update
        public void Update(OntimeDelay ontimeDelay) {
            objDb.Update(ontimeDelay);
        }

        //Delete
        public void Delete(string deliveryNote) {
            objDb.Delete(deliveryNote);
        }
    }
}
