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
        public IQueryable<BOLDropdownLists> GetByUser(int month, int year)
        {
            return objDb.GetByUser(month, year);
        }
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
        //GetAll
        public IQueryable<TenderedAdjusted> GetAll() {
            return objDb.GetAll();
        }
        //GetByFilter
        public IQueryable<TenderedAdjusted> GetByFilter(string segment_id,int year,int month)
        {
            return objDb.GetByFilter(segment_id,year,month);
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
