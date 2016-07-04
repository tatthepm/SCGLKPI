using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class AcceptedPendingBs {
        private AcceptedPendingDb objDb;
        public AcceptedPendingBs() {
            objDb = new AcceptedPendingDb();
        }
        //GetAll
        public IEnumerable<AcceptedPending> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public AcceptedPending GetByID(string shipmentNo) {
            return objDb.GetByID(shipmentNo);
        }

        //Insert
        public void Insert(AcceptedPending acceptedPending) {
            objDb.Insert(acceptedPending);
        }

        //Update
        public void Update(AcceptedPending acceptedPending) {
            objDb.Update(acceptedPending);
        }

        //Delete
        public void Delete(string shipmentNo) {
            objDb.Delete(shipmentNo);
        }
    }
}
