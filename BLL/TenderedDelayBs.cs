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
        public IQueryable<BOLDropdownLists> GetByUser(int month, int year)
        {
            return objDb.GetByUser(month, year);
        }
        //GetAll
        public IQueryable<TenderedDelay> GetAll() {
            return objDb.GetAll();
        }
        //GetBySegment
        public IQueryable<BOLDropdownLists> GetByShipto(string segment)
        {
            return objDb.GetByShipto(segment);
        }
        //GetByShipPoint
        public IQueryable<BOLDropdownLists> GetByShipPoint(string segment)
        {
            return objDb.GetByShipPoint(segment);
        }
        //GetByTruckType
        public IQueryable<BOLDropdownLists> GetByTruckType(string segment)
        {
            return objDb.GetByTruckType(segment);
        }
        //GetByFilter
        public IQueryable<TenderedDelay> GetByFilter(string segment_id, int year, int month)
        {
            return objDb.GetByFilter(segment_id, year, month);
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
