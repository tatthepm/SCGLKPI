using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class AcceptedDelayBs {
        private AcceptedDelayDb objDb;
        public AcceptedDelayBs() {
            objDb = new AcceptedDelayDb();
        }
        //GetAll
        public IEnumerable<AcceptedDelay> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public AcceptedDelay GetByID(string shipmentNo) {
            return objDb.GetByID(shipmentNo);
        }

        //Insert
        public void Insert(AcceptedDelay acceptedDelay) {
            objDb.Insert(acceptedDelay);
        }

        //Update
        public void Update(AcceptedDelay acceptedDelay) {
            objDb.Update(acceptedDelay);
        }

        //Delete
        public void Delete(string shipmentNo) {
            objDb.Delete(shipmentNo);
        }
    }
}
