using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OutboundDelayBs {
        private OutboundDelayDb objDb;
        public OutboundDelayBs() {
            objDb = new OutboundDelayDb();
        }
        //GetAll
        public IEnumerable<OutboundDelay> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OutboundDelay GetByID(string deliveryNote) {
            return objDb.GetByID(deliveryNote);
        }

        //Insert
        public void Insert(OutboundDelay outboundDelay) {
            objDb.Insert(outboundDelay);
        }

        //Update
        public void Update(OutboundDelay outboundDelay) {
            objDb.Update(outboundDelay);
        }

        //Delete
        public void Delete(string deliveryNote) {
            objDb.Delete(deliveryNote);
        }
    }
}
