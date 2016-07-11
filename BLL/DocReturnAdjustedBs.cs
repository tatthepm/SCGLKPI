using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class DocReturnAdjustedBs {
        private DocReturnAdjustedDb objDb;
        public DocReturnAdjustedBs() {
            objDb = new DocReturnAdjustedDb();
        }
        //GetAll
        public IEnumerable<DocReturnAdjusted> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public DocReturnAdjusted GetByID(string deliveryNote) {
            return objDb.GetByID(deliveryNote);
        }

        //Insert
        public void Insert(DocReturnAdjusted docReturnAdjusted) {
            objDb.Insert(docReturnAdjusted);
        }

        //Update
        public void Update(DocReturnAdjusted docReturnAdjusted) {
            objDb.Update(docReturnAdjusted);
        }

        //Delete
        public void Delete(string deliveryNote) {
            objDb.Delete(deliveryNote);
        }
    }
}
