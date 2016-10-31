using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OutboundAdjustedDb {
        private SCGLKPIDbContext db;
        public OutboundAdjustedDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<OutboundAdjusted> GetAll() {
            return db.OutboundAdjusted;
        }

        //GetByFilter
        public IQueryable<OutboundAdjusted> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.OutboundAdjusted.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.ACTGIDATE_D.Value.Year == year && x.ACTGIDATE_D.Value.Month == month);
        }
        //GetById
        public OutboundAdjusted GetByID(string deliveryNote)
        {
            return db.OutboundAdjusted.Find(deliveryNote);
        }
        //GetByMatName
        public IQueryable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.OutboundAdjusted
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
            var Queryable = (from m in db.OutboundAdjusted
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
            var Queryable = (from m in db.OutboundAdjusted
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
            var Queryable = (from m in db.OutboundAdjusted
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct();
            return Queryable;
        }

        //Insert
        public void Insert(OutboundAdjusted outboundAdjusted) {
            db.OutboundAdjusted.Add(outboundAdjusted);
            Save();
        }

        //Update
        public void Update(OutboundAdjusted outboundAdjusted) {
            db.Entry(outboundAdjusted).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            OutboundAdjusted outboundAdjusted = db.OutboundAdjusted.Find(deliveryNote);
            db.OutboundAdjusted.Remove(outboundAdjusted);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
