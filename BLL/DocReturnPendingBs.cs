using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class DocReturnPendingBs {
        private DocReturnPendingDb objDb;
        public DocReturnPendingBs() {
            objDb = new DocReturnPendingDb();
        }
        //GetAll
        public IEnumerable<DocReturnPending> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public DocReturnPending GetByID(string deliveryNote) {
            return objDb.GetByID(deliveryNote);
        }

        //Insert
        public void Insert(DocReturnPending docReturnPending) {
            objDb.Insert(docReturnPending);
        }

        //Update
        public void Update(DocReturnPending docReturnPending) {
            objDb.Update(docReturnPending);
        }

        //Delete
        public void Delete(string deliveryNote) {
            objDb.Delete(deliveryNote);
        }
    }
}
