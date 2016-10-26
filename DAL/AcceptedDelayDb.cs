using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Linq;
using BOL;

namespace DAL {
   public class AcceptedDelayDb {
        private SCGLKPIDbContext db;
        public AcceptedDelayDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<AcceptedDelay> GetAll() {
            return db.AcceptedDelays;
        }
        //GetByFilter
        public IQueryable<AcceptedDelay> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.AcceptedDelays.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.LACPDDATE_D.Value.Year == year && x.LACPDDATE_D.Value.Month == month).Take(1000);
        }
        //GetById
        public AcceptedDelay GetByID(string shipmentNo)
        {
            return db.AcceptedDelays.Find(shipmentNo);
        }
        //GetByMatName
        public IQueryable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.AcceptedDelays
                             select new BOLDropdownLists
                             {
                                 Id = m.MATFRIGRP,
                                 Name = m.MATNAME,
                             }).Distinct();
            return Queryable;
        }

        //GetByMatName (Overload)
        public IQueryable<BOLDropdownLists> GetByMatName(string departmentId, string sectionId)
        {
            var Queryable = (from m in db.AcceptedDelays
                             where m.DEPARTMENT_ID == departmentId && m.SECTION_ID == sectionId
                             select new BOLDropdownLists
                             {
                                 Id = m.MATFRIGRP,
                                 Name = m.MATNAME,
                             }).Distinct();
            return Queryable;
        }

        //GetBySection
        public IQueryable<BOLDropdownLists> GetBySection()
        {
            var Queryable = (from m in db.AcceptedDelays
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct();
            return Queryable;
        }
        //GetBySection (Overload)
        public IQueryable<BOLDropdownLists> GetBySection(string departmentId)
        {
            var Queryable = (from m in db.AcceptedDelays
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct();
            return Queryable;
        }
        //Insert
        public void Insert(AcceptedDelay acceptedDelay) {
            db.AcceptedDelays.Add(acceptedDelay);
            Save();
        }

        //Update
        public void Update(AcceptedDelay acceptedDelay) {
            db.Entry(acceptedDelay).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string shipmentNo) {
            AcceptedDelay acceptedDelay = db.AcceptedDelays.Find(shipmentNo);
            db.AcceptedDelays.Remove(acceptedDelay);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
