using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class TenderedPendingDb {
        private SCGLKPIDbContext db;
        public TenderedPendingDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<TenderPending> GetAll() {
            return db.TenderPendings;
        }
        //GetByShipto
        public IQueryable<BOLDropdownLists> GetByShipto(string segment)
        {
            var Queryable = (from m in db.TenderPendings
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
            var Queryable = (from m in db.TenderPendings
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
            var Queryable = (from m in db.TenderPendings
                             where m.SUBSEGMENT == segment
                             select new BOLDropdownLists
                             {
                                 Id = m.TRUCK_TYPE,
                                 Name = m.TRUCK_TYPE,
                             }).Distinct();
            return Queryable;
        }
        //GetByFilter
        public IQueryable<TenderPending> GetByFilter(string segment_id, int month, int year)
        {
            return db.TenderPendings.Where(x => x.SUBSEGMENT == segment_id && x.PLNTNRDDATE_D.Value.Year == year && x.PLNTNRDDATE_D.Value.Month == month);
        }
        //GetById
        public TenderPending GetByID(string shipmentNo) {
            return db.TenderPendings.Find(shipmentNo);
        }

        //Insert
        public void Insert(TenderPending tenderedPending) {
            db.TenderPendings.Add(tenderedPending);
            Save();
        }

        //Update
        public void Update(TenderPending tenderedPending) {
            db.Entry(tenderedPending).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string shipmentNo) {
            TenderPending tenderedPending = db.TenderPendings.Find(shipmentNo);
            db.TenderPendings.Remove(tenderedPending);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
