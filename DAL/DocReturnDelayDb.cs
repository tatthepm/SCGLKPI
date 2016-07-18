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
        public IEnumerable<DocReturnDelay> GetAll() {
            return db.DocReturnDelays.ToList();
        }
        //GetByFilter
        public IEnumerable<DocReturnDelay> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.DocReturnDelays.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.DOCRETDATE_SCGL_D.Value.Year == year && x.DOCRETDATE_SCGL_D.Value.Month == month);
        }
        //GetById
        public DocReturnDelay GetByID(string deliveryNote) {
            return db.DocReturnDelays.Find(deliveryNote);
        }
        //GetByMatName
        public IEnumerable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.DocReturnDelays
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
            var Queryable = (from m in db.DocReturnDelays
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
            var Queryable = (from m in db.DocReturnDelays
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
            var Queryable = (from m in db.DocReturnDelays
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct().ToList();
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
