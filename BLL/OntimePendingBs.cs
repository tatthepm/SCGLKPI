using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;


namespace BLL {
    public class OntimePendingBs {
        private OntimePendingDb objDb;
        public OntimePendingBs() {
            objDb = new OntimePendingDb();
        }
        //GetAll
        public IEnumerable<OntimePending> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimePending GetByID(string deliveryNote) {
            return objDb.GetByID(deliveryNote);
        }

        //Insert
        public void Insert(OntimePending ontimePending) {
            objDb.Insert(ontimePending);
        }

        //Update
        public void Update(OntimePending ontimePending) {
            objDb.Update(ontimePending);
        }

        //Delete
        public void Delete(string deliveryNote) {
            objDb.Delete(deliveryNote);
        }
    }
}
