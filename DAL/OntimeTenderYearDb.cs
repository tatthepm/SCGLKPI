using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;


namespace DAL {
    public class OntimeTenderYearDb {
        private SCGLKPIDbContext db;
        public OntimeTenderYearDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<OntimeTenderYear> GetAll() {
            return db.OntimeTenderYears;
        }
        //GetByShipto
        public IQueryable<BOLDropdownLists> GetByShipto(string segment)
        {
            var Queryable = (from m in db.OntimeTenderYears
                             where m.SubSegment == segment
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
            var Queryable = (from m in db.OntimeTenderYears
                             where m.SubSegment == segment
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
            var Queryable = (from m in db.OntimeTenderYears
                             where m.SubSegment == segment
                             select new BOLDropdownLists
                             {
                                 Id = m.TRUCK_TYPE,
                                 Name = m.TRUCK_TYPE,
                             }).Distinct();
            return Queryable;
        }
        //GetById
        public OntimeTenderYear GetByID(int Id) {
            return db.OntimeTenderYears.Find(Id);
        }

        //Insert
        public void Insert(OntimeTenderYear ontimeTenderYear) {
            db.OntimeTenderYears.Add(ontimeTenderYear);
            Save();
        }

        //Update
        public void Update(OntimeTenderYear ontimeTenderYear) {
            db.Entry(ontimeTenderYear).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeTenderYear ontimeTenderYear = db.OntimeTenderYears.Find(Id);
            db.OntimeTenderYears.Remove(ontimeTenderYear);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
