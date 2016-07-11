using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OutboundAdjustedBs {
        private OutboundAdjustedDb objDb;
        public OutboundAdjustedBs() {
            objDb = new OutboundAdjustedDb();
        }
        //GetAll
        public IEnumerable<OutboundAdjusted> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OutboundAdjusted GetByID(string deliveryNote) {
            return objDb.GetByID(deliveryNote);
        }

        //Insert
        public void Insert(OutboundAdjusted outboundAdjusted) {
            objDb.Insert(outboundAdjusted);
        }

        //Update
        public void Update(OutboundAdjusted outboundAdjusted) {
            objDb.Update(outboundAdjusted);
        }

        //Delete
        public void Delete(string deliveryNote) {
            objDb.Delete(deliveryNote);
        }
    }
}
