using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class InboundPendingBs {
        private InboundPendingDb objDb;
        public InboundPendingBs() {
            objDb = new InboundPendingDb();
        }
        //GetAll
        public IEnumerable<InboundPending> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public InboundPending GetByID(string deliveryNote) {
            return objDb.GetByID(deliveryNote);
        }

        //Insert
        public void Insert(InboundPending inboundPending) {
            objDb.Insert(inboundPending);
        }

        //Update
        public void Update(InboundPending inboundPending) {
            objDb.Update(inboundPending);
        }

        //Delete
        public void Delete(string deliveryNote) {
            objDb.Delete(deliveryNote);
        }
    }
}
