using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class AcceptPendingBs {
        private AcceptedPendingDb objDb;
        public AcceptPendingBs() {
            objDb = new AcceptedPendingDb();
        }
        //GetAll
        public IEnumerable<AcceptPending> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public AcceptPending GetByID(string shipmentNo) {
            return objDb.GetByID(shipmentNo);
        }

        //Insert
        public void Insert(AcceptPending acceptedPending) {
            objDb.Insert(acceptedPending);
        }

        //Update
        public void Update(AcceptPending acceptedPending) {
            objDb.Update(acceptedPending);
        }

        //Delete
        public void Delete(string shipmentNo) {
            objDb.Delete(shipmentNo);
        }
    }
}
