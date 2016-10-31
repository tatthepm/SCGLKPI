using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OutboundDelayDb {
        private SCGLKPIDbContext db;
        public OutboundDelayDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<OutboundDelay> GetAll() {
            return db.OutboundDelays;
        }
        //GetByFilter
        public IQueryable<OutboundDelay> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.OutboundDelays.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.ACTGIDATE_D.Value.Year == year && x.ACTGIDATE_D.Value.Month == month);
        }
        //GetById
        public OutboundDelay GetByID(string deliveryNote) {
            return db.OutboundDelays.Find(deliveryNote);
        }
        //GetByMatName
        public IQueryable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.OutboundDelays
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
            var Queryable = (from m in db.OutboundDelays
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
            var Queryable = (from m in db.OutboundDelays
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
            var Queryable = (from m in db.OutboundDelays
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct();
            return Queryable;
        }
        //Insert
        public void Insert(OutboundDelay outboundDelay) {
            db.OutboundDelays.Add(outboundDelay);
            Save();
        }

        //Update
        public void Update(OutboundDelay outboundDelay) {
            db.Entry(outboundDelay).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            OutboundDelay outboundDelay = db.OutboundDelays.Find(deliveryNote);
            db.OutboundDelays.Remove(outboundDelay);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
