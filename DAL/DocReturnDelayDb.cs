using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class DocReturnDelayDb {
        private SCGLKPIDbContext db;
        public DocReturnDelayDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<DocReturnDelay> GetAll() {
            return db.DocReturnDelays;
        }
        //GetByFilter
        public IQueryable<DocReturnDelay> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.DocReturnDelays.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.ACTGIDATE_D.Value.Year == year && x.ACTGIDATE_D.Value.Month == month);
        }
        //GetById
        public DocReturnDelay GetByID(string deliveryNote) {
            return db.DocReturnDelays.Find(deliveryNote);
        }
        //GetByMatName
        public IQueryable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.DocReturnDelays
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
            var Queryable = (from m in db.DocReturnDelays
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
            var Queryable = (from m in db.DocReturnDelays
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
            var Queryable = (from m in db.DocReturnDelays
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct();
            return Queryable;
        }
        //Insert
        public void Insert(DocReturnDelay outboundDelay) {
            db.DocReturnDelays.Add(outboundDelay);
            Save();
        }

        //Update
        public void Update(DocReturnDelay docReturnDelay) {
            db.Entry(docReturnDelay).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            DocReturnDelay docReturnDelay = db.DocReturnDelays.Find(deliveryNote);
            db.DocReturnDelays.Remove(docReturnDelay);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
