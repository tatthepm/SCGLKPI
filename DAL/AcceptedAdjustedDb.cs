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
        public IEnumerable<AcceptedAdjusted> GetAll() {
            return db.AcceptedAdjusted.ToList();
        }

        //GetByFilter
        public IEnumerable<AcceptedAdjusted> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.AcceptedAdjusted.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.PLNACPDDATE_D.Value.Year == year && x.PLNACPDDATE_D.Value.Month == month);
        }
        //GetById
        public AcceptedAdjusted GetByID(string shipmentNo)
        {
            return db.AcceptedAdjusted.Find(shipmentNo);
        }
        //GetByMatName
        public IEnumerable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.AcceptedAdjusted
                             select new BOLDropdownLists
                             {
                                 Id = m.MATFRIGRP,
                                 Name = m.MATNAME,
                             }).Distinct().ToList();
            return Queryable;
        }

        //GetByMatName (Overload)
        public IEnumerable<BOLDropdownLists> GetByMatName(string departmentId, string sectionId)
        {
            var Queryable = (from m in db.AcceptedAdjusted
                             where m.DEPARTMENT_ID == departmentId && m.SECTION_ID == sectionId
                             select new BOLDropdownLists
                             {
                                 Id = m.MATFRIGRP,
                                 Name = m.MATNAME,
                             }).Distinct().ToList();
            return Queryable;
        }

        //GetBySection
        public IEnumerable<BOLDropdownLists> GetBySection()
        {
            var Queryable = (from m in db.AcceptedAdjusted
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct().ToList();
            return Queryable;
        }
        //GetBySection (Overload)
        public IEnumerable<BOLDropdownLists> GetBySection(string departmentId)
        {
            var Queryable = (from m in db.AcceptedAdjusted
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct().ToList();
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
