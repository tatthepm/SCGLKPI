using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class InboundAdjustedDb {
        private SCGLKPIDbContext db;
        public InboundAdjustedDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<InboundAdjusted> GetAll() {
            return db.InboundAdjusted;
        }

        //GetByFilter
        public IQueryable<InboundAdjusted> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.InboundAdjusted.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.ACTGIDATE_D.Value.Year == year && x.ACTGIDATE_D.Value.Month == month);
        }

        //GetById
        public InboundAdjusted GetByID(string deliveryNote)
        {
            return db.InboundAdjusted.Find(deliveryNote);
        }
        //GetByMatName
        public IQueryable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.InboundAdjusted
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
            var Queryable = (from m in db.InboundAdjusted
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
            var Queryable = (from m in db.InboundAdjusted
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
            var Queryable = (from m in db.InboundAdjusted
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct();
            return Queryable;
        }

        //Insert
        public void Insert(InboundAdjusted inboundAdjusted) {
            db.InboundAdjusted.Add(inboundAdjusted);
            Save();
        }

        //Update
        public void Update(InboundAdjusted inboundAdjusted) {
            db.Entry(inboundAdjusted).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            InboundAdjusted inboundAdjusted = db.InboundAdjusted.Find(deliveryNote);
            db.InboundAdjusted.Remove(inboundAdjusted);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
