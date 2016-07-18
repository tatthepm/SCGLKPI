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
        //GetByFilter
        public IEnumerable<TenderedAdjusted> GetByFilter(string segment_id, int month, int year)
        {
            return objDb.GetByFilter(segment_id, month, year);
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
