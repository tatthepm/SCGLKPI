using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class InboundAdjustedBs {
        private InboundAdjustedDb objDb;
        public InboundAdjustedBs() {
            objDb = new InboundAdjustedDb();
        }
        //GetAll
        public IEnumerable<InboundAdjusted> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public InboundAdjusted GetByID(string deliveryNote) {
            return objDb.GetByID(deliveryNote);
        }

        //Insert
        public void Insert(InboundAdjusted inboundAdjusted) {
            objDb.Insert(inboundAdjusted);
        }

        //Update
        public void Update(InboundAdjusted inboundAdjusted) {
            objDb.Update(inboundAdjusted);
        }

        //Delete
        public void Delete(string deliveryNote) {
            objDb.Delete(deliveryNote);
        }
    }
}
