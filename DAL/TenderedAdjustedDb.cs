using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class TenderedAdjustedDb {
        private SCGLKPIDbContext db;
        public TenderedAdjustedDb() {
            db = new SCGLKPIDbContext();
        }
        
        //GetAll
        public IQueryable<TenderedAdjusted> GetAll() {
            return db.TenderedAdjusted;
        }

        //GetByFilter
        public IQueryable<TenderedAdjusted> GetByFilter(string segment_id, int year, int month)
        {
            return db.TenderedAdjusted.Where(x => x.SUBSEGMENT == segment_id && x.PLNTNRDDATE_D.Value.Year == year && x.PLNTNRDDATE_D.Value.Month == month);
        }

        //GetByShipto
        public IQueryable<BOLDropdownLists> GetByShipto(string segment)
        {
            var Queryable = (from m in db.TenderedAdjusted
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
            var Queryable = (from m in db.TenderedAdjusted
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
            var Queryable = (from m in db.TenderedAdjusted
                             where m.SUBSEGMENT == segment
                             select new BOLDropdownLists
                             {
                                 Id = m.TRUCK_TYPE,
                                 Name = m.TRUCK_TYPE,
                             }).Distinct();
            return Queryable;
        }

        //GetById
        public TenderedAdjusted GetByID(string shipmentNo) {
            return db.TenderedAdjusted.Find(shipmentNo);
        }

        //Insert
        public void Insert(TenderedAdjusted tenderedAdjusted) {
            db.TenderedAdjusted.Add(tenderedAdjusted);
            Save();
        }

        //Update
        public void Update(TenderedAdjusted tenderedAdjusted) {
            db.Entry(tenderedAdjusted).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string shipmentNo) {
            TenderedAdjusted tenderedAdjusted = db.TenderedAdjusted.Find(shipmentNo);
            db.TenderedAdjusted.Remove(tenderedAdjusted);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
