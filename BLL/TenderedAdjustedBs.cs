using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class TenderedAdjustedBs {
        private TenderedAdjustedDb objDb;
        public TenderedAdjustedBs() {
            objDb = new TenderedAdjustedDb();
        }
        //GetAll
        public IEnumerable<TenderedAdjusted> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public TenderedAdjusted GetByID(string shipmentNo) {
            return objDb.GetByID(shipmentNo);
        }

        //Insert
        public void Insert(TenderedAdjusted tenderedAdjusted) {
            objDb.Insert(tenderedAdjusted);
        }

        //Update
        public void Update(TenderedAdjusted tenderedAdjusted) {
            objDb.Update(tenderedAdjusted);
        }

        //Delete
        public void Delete(string shipmentNo) {
            objDb.Delete(shipmentNo);
        }
    }
}
