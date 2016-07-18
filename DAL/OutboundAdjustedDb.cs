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
        public IEnumerable<OutboundAdjusted> GetAll() {
            return db.OutboundAdjusted.ToList();
        }

        //GetByFilter
        public IEnumerable<OutboundAdjusted> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.OutboundAdjusted.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.PLNOUTBDATE_D.Value.Year == year && x.PLNOUTBDATE_D.Value.Month == month);
        }
        //GetById
        public OutboundAdjusted GetByID(string deliveryNote)
        {
            return db.OutboundAdjusted.Find(deliveryNote);
        }
        //GetByMatName
        public IEnumerable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.OutboundAdjusted
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
            var Queryable = (from m in db.OutboundAdjusted
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
            var Queryable = (from m in db.OutboundAdjusted
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
            var Queryable = (from m in db.OutboundAdjusted
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct().ToList();
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
