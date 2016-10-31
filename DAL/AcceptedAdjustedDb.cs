using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
   public class AcceptedAdjustedDb {
        private SCGLKPIDbContext db;
        public AcceptedAdjustedDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<AcceptedAdjusted> GetAll() {
            return db.AcceptedAdjusted;
        }

        //GetByFilter
        public IQueryable<AcceptedAdjusted> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.AcceptedAdjusted.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.LACPDDATE_D.Value.Year == year && x.LACPDDATE_D.Value.Month == month);
        }
        //GetById
        public AcceptedAdjusted GetByID(string shipmentNo)
        {
            return db.AcceptedAdjusted.Find(shipmentNo);
        }
        //GetByMatName
        public IQueryable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.AcceptedAdjusted
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
            var Queryable = (from m in db.AcceptedAdjusted
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
            var Queryable = (from m in db.AcceptedAdjusted
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
            var Queryable = (from m in db.AcceptedAdjusted
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct();
            return Queryable;
        }

        //Insert
        public void Insert(AcceptedAdjusted acceptedAdjusted) {
            db.AcceptedAdjusted.Add(acceptedAdjusted);
            Save();
        }

        //Update
        public void Update(AcceptedAdjusted acceptedAdjusted) {
            db.Entry(acceptedAdjusted).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string shipmentNo) {
            AcceptedAdjusted acceptedAdjusted = db.AcceptedAdjusted.Find(shipmentNo);
            db.AcceptedAdjusted.Remove(acceptedAdjusted);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
