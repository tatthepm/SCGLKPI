using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class InboundDelayBs {
        private InboundDelayDb objDb;
        public InboundDelayBs() {
            objDb = new InboundDelayDb();
        }
        //GetAll
        public IEnumerable<InboundDelay> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public InboundDelay GetByID(string deliveryNote) {
            return objDb.GetByID(deliveryNote);
        }

        //Insert
        public void Insert(InboundDelay inboundDelay) {
            objDb.Insert(inboundDelay);
        }

        //Update
        public void Update(InboundDelay inboundDelay) {
            objDb.Update(inboundDelay);
        }

        //Delete
        public void Delete(string deliveryNote) {
            objDb.Delete(deliveryNote);
        }
    }
}
