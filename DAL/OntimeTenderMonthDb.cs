using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeTenderMonthDb {
        private SCGLKPIDbContext db;
        public OntimeTenderMonthDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<OntimeTenderMonth> GetAll() {
            return db.OntimeTenderMonths;
        }
        //GetByShipto
        public IQueryable<BOLDropdownLists> GetByShipto(string segment)
        {
            var Queryable = (from m in db.OntimeTenderMonths
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
            var Queryable = (from m in db.OntimeTenderMonths
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
            var Queryable = (from m in db.OntimeTenderMonths
                             where m.SubSegment == segment
                             select new BOLDropdownLists
                             {
                                 Id = m.TRUCK_TYPE,
                                 Name = m.TRUCK_TYPE,
                             }).Distinct();
            return Queryable;
        }
        //GetById
        public OntimeTenderMonth GetByID(int Id) {
            return db.OntimeTenderMonths.Find(Id);
        }

        //Insert
        public void Insert(OntimeTenderMonth ontimeTenderMonth) {
            db.OntimeTenderMonths.Add(ontimeTenderMonth);
            Save();
        }

        //Update
        public void Update(OntimeTenderMonth ontimeTenderMonth) {
            db.Entry(ontimeTenderMonth).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeTenderMonth ontimeTenderMonth = db.OntimeTenderMonths.Find(Id);
            db.OntimeTenderMonths.Remove(ontimeTenderMonth);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
