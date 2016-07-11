using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class AcceptedAdjustedBs {
        private AcceptedAdjustedDb objDb;
        public AcceptedAdjustedBs() {
            objDb = new AcceptedAdjustedDb();
        }
        //GetAll
        public IEnumerable<AcceptedAdjusted> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public AcceptedAdjusted GetByID(string shipmentNo) {
            return objDb.GetByID(shipmentNo);
        }

        //Insert
        public void Insert(AcceptedAdjusted acceptedAdjusted) {
            objDb.Insert(acceptedAdjusted);
        }

        //Update
        public void Update(AcceptedAdjusted acceptedAdjusted) {
            objDb.Update(acceptedAdjusted);
        }

        //Delete
        public void Delete(string shipmentNo) {
            objDb.Delete(shipmentNo);
        }
    }
}
