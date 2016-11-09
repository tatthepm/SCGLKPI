using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeTenderDb {
        private SCGLKPIDbContext db;
        public OntimeTenderDb() {
            db = new SCGLKPIDbContext();
        }

        //GetAll
        public IQueryable<OntimeTender> GetAll() {
            return db.OntimeTenders;
        }
        //GetByShipto
        public IQueryable<BOLDropdownLists> GetByShipto(string segment)
        {
            var Queryable = (from m in db.OntimeTenders
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
            var Queryable = (from m in db.OntimeTenders
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
            var Queryable = (from m in db.OntimeTenders
                             where m.SubSegment == segment
                             select new BOLDropdownLists
                             {
                                 Id = m.TRUCK_TYPE,
                                 Name = m.TRUCK_TYPE,
                             }).Distinct();
            return Queryable;
        }
        //GetById
        public OntimeTender GetByID(int Id) {
            return db.OntimeTenders.Find(Id);
        }

        //Insert
        public void Insert(OntimeTender ontimeTender) {
            db.OntimeTenders.Add(ontimeTender);
            Save();
        }

        //Update
        public void Update(OntimeTender ontimeTender) {
            db.Entry(ontimeTender).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeTender ontimeTender = db.OntimeTenders.Find(Id);
            db.OntimeTenders.Remove(ontimeTender);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
