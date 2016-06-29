using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class TenderedDelayBs {
        private TenderedDelayDb objDb;
        public TenderedDelayBs() {
            objDb = new TenderedDelayDb();
        }
        //GetAll
        public IEnumerable<TenderedDelay> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public TenderedDelay GetByID(string shipmentNo) {
            return objDb.GetByID(shipmentNo);
        }

        //Insert
        public void Insert(TenderedDelay tenderedDelay) {
            objDb.Insert(tenderedDelay);
        }

        //Update
        public void Update(TenderedDelay tenderedDelay) {
            objDb.Update(tenderedDelay);
        }

        //Delete
        public void Delete(string shipmentNo) {
            objDb.Delete(shipmentNo);
        }
    }
}
