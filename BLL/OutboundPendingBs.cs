using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OutboundPendingBs {
        private OutboundPendingDb objDb;
        public OutboundPendingBs() {
            objDb = new OutboundPendingDb();
        }
        //GetAll
        public IEnumerable<OutboundPending> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OutboundPending GetByID(string deliveryNote) {
            return objDb.GetByID(deliveryNote);
        }

        //Insert
        public void Insert(OutboundPending outboundPending) {
            objDb.Insert(outboundPending);
        }

        //Update
        public void Update(OutboundPending outboundPending) {
            objDb.Update(outboundPending);
        }

        //Delete
        public void Delete(string deliveryNote) {
            objDb.Delete(deliveryNote);
        }
    }
}
