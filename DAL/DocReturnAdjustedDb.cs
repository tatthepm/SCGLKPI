using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class DocReturnAdjustedDb {
        private SCGLKPIDbContext db;
        public DocReturnAdjustedDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<DocReturnAdjusted> GetAll() {
            return db.DocReturnAdjusted.ToList();
        }
        //GetByFilter
        public IEnumerable<DocReturnAdjusted> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.DocReturnAdjusted.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.PLNDOCRETDATE_SCGL_D.Value.Year == year && x.PLNDOCRETDATE_SCGL_D.Value.Month == month);
        }
        //GetById
        public DocReturnAdjusted GetByID(string deliveryNote)
        {
            return db.DocReturnAdjusted.Find(deliveryNote);
        }
        //GetByMatName
        public IEnumerable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.DocReturnAdjusted
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
            var Queryable = (from m in db.DocReturnAdjusted
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
            var Queryable = (from m in db.DocReturnAdjusted
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
            var Queryable = (from m in db.DocReturnAdjusted
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct().ToList();
            return Queryable;
        }

        //Insert
        public void Insert(DocReturnAdjusted outboundAdjusted) {
            db.DocReturnAdjusted.Add(outboundAdjusted);
            Save();
        }

        //Update
        public void Update(DocReturnAdjusted docReturnAdjusted) {
            db.Entry(docReturnAdjusted).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            DocReturnAdjusted docReturnAdjusted = db.DocReturnAdjusted.Find(deliveryNote);
            db.DocReturnAdjusted.Remove(docReturnAdjusted);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
