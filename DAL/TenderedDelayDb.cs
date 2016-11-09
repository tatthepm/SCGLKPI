using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class TenderedDelayDb {
        private SCGLKPIDbContext db;
        public TenderedDelayDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<TenderedDelay> GetAll() {
            return db.TenderedDelays;
        }
        //GetByShipto
        public IQueryable<BOLDropdownLists> GetByShipto(string segment)
        {
            var Queryable = (from m in db.TenderedDelays
                             where m.SUBSEGMENT == segment
                             select new BOLDropdownLists
                             {
                                 Id = m.SHIPTO,
                                 Name = m.SHIPTO,
                             }).Distinct();
            return Queryable;
        }
        //GetByShipPoint
        public IQueryable<BOLDropdownLists> GetByShipPoint(string segment)
        {
            var Queryable = (from m in db.TenderedDelays
                             where m.SUBSEGMENT == segment
                             select new BOLDropdownLists
                             {
                                 Id = m.SHPPOINT,
                                 Name = m.SHPPOINT,
                             }).Distinct();
            return Queryable;
        }
        //GetByTruckType
        public IQueryable<BOLDropdownLists> GetByTruckType(string segment)
        {
            var Queryable = (from m in db.TenderedDelays
                             where m.SUBSEGMENT == segment
                             select new BOLDropdownLists
                             {
                                 Id = m.TRUCK_TYPE,
                                 Name = m.TRUCK_TYPE,
                             }).Distinct();
            return Queryable;
        }
        //GetByFilter
        public IQueryable<TenderedDelay> GetByFilter(string segment_id, int year , int month)
        {
            return db.TenderedDelays.Where(x => x.SUBSEGMENT == segment_id && x.PLNTNRDDATE_D.Value.Year == year && x.PLNTNRDDATE_D.Value.Month == month);
        }
        //GetById
        public TenderedDelay GetByID(string shipmentNo) {
            return db.TenderedDelays.Find(shipmentNo);
        }
        //Insert
        public void Insert(TenderedDelay tenderedDelay) {
            db.TenderedDelays.Add(tenderedDelay);
            Save();
        }

        //Update
        public void Update(TenderedDelay tenderedDelay) {
            db.Entry(tenderedDelay).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string shipmentNo) {
            TenderedDelay tenderedDelay = db.TenderedDelays.Find(shipmentNo);
            db.TenderedDelays.Remove(tenderedDelay);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
