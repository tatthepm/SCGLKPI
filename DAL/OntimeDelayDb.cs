using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeDelayDb {
        private SCGLKPIDbContext db;
        public OntimeDelayDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<OntimeDelay> GetAll() {
            return db.OntimeDelays;
        }
        //GetByFilter
        public IQueryable<OntimeDelay> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.OntimeDelays.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.ACTGIDATE_D.Value.Year == year && x.ACTGIDATE_D.Value.Month == month);
        }

        //GetById
        public OntimeDelay GetByID(string deliveryNote) {
            return db.OntimeDelays.Find(deliveryNote);
        }

        //GetByMatName
        public IQueryable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.OntimeDelays
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
            var Queryable = (from m in db.OntimeDelays
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
            var Queryable = (from m in db.OntimeDelays
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
            var Queryable = (from m in db.OntimeDelays
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct();
            return Queryable;
        }

        //Insert
        public void Insert(OntimeDelay ontimeDelay) {
            db.OntimeDelays.Add(ontimeDelay);
            Save();
        }

        //Update
        public void Update(OntimeDelay ontimeDelay) {
            db.Entry(ontimeDelay).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            OntimeDelay ontimeDelay = db.OntimeDelays.Find(deliveryNote);
            db.OntimeDelays.Remove(ontimeDelay);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
