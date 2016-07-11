using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;


namespace BLL {
    public class OntimeAdjustedBs {
        private OntimeAdjustedDb objDb;
        public OntimeAdjustedBs() {
            objDb = new OntimeAdjustedDb();
        }
        //GetAll
        public IEnumerable<OntimeAdjusted> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeAdjusted GetByID(string deliveryNote) {
            return objDb.GetByID(deliveryNote);
        }

        //Insert
        public void Insert(OntimeAdjusted ontimeAdjusted) {
            objDb.Insert(ontimeAdjusted);
        }

        //Update
        public void Update(OntimeAdjusted ontimeAdjusted) {
            objDb.Update(ontimeAdjusted);
        }

        //Delete
        public void Delete(string deliveryNote) {
            objDb.Delete(deliveryNote);
        }
    }
}
