using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class DocReturnDelayBs {
        private DocReturnDelayDb objDb;
        public DocReturnDelayBs() {
            objDb = new DocReturnDelayDb();
        }
        //GetAll
        public IEnumerable<DocReturnDelay> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public DocReturnDelay GetByID(string deliveryNote) {
            return objDb.GetByID(deliveryNote);
        }

        //Insert
        public void Insert(DocReturnDelay docReturnDelay) {
            objDb.Insert(docReturnDelay);
        }

        //Update
        public void Update(DocReturnDelay docReturnDelay) {
            objDb.Update(docReturnDelay);
        }

        //Delete
        public void Delete(string deliveryNote) {
            objDb.Delete(deliveryNote);
        }
    }
}
