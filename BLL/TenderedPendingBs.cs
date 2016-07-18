using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class TenderedPendingBs {
        private TenderedPendingDb objDb;
        public TenderedPendingBs() {
            objDb = new TenderedPendingDb();
        }
        //GetAll
        public IEnumerable<TenderPending> GetAll() {
            return objDb.GetAll();
        }
        //GetByFilter
        public IEnumerable<TenderPending> GetByFilter(string segment_id, int month, int year)
        {
            return objDb.GetByFilter(segment_id, month, year);
        }
        //GetById
        public TenderPending GetByID(string shipmentNo) {
            return objDb.GetByID(shipmentNo);
        }

        //Insert
        public void Insert(TenderPending tenderedPending) {
            objDb.Insert(tenderedPending);
        }

        //Update
        public void Update(TenderPending tenderedPending) {
            objDb.Update(tenderedPending);
        }

        //Delete
        public void Delete(string shipmentNo) {
            objDb.Delete(shipmentNo);
        }
    }
}
