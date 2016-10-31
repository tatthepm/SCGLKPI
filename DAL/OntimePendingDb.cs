using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimePendingDb {
        private SCGLKPIDbContext db;
        public OntimePendingDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<OntimePending> GetAll() {
            return db.OntimePendings;
        }

        //GetByFilter
        public IQueryable<OntimePending> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.OntimePendings.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.PLNONTIMEDATE_D.Value.Year == year && x.PLNONTIMEDATE_D.Value.Month == month);
        }

        //GetById
        public OntimePending GetByID(string deliveryNote)
        {
            return db.OntimePendings.Find(deliveryNote);
        }

        //GetByMatName
        public IQueryable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.OntimePendings
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
            var Queryable = (from m in db.OntimePendings
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
            var Queryable = (from m in db.OntimePendings
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
            var Queryable = (from m in db.OntimePendings
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct();
            return Queryable;
        }

        //Insert
        public void Insert(OntimePending ontimePending) {
            db.OntimePendings.Add(ontimePending);
            Save();
        }

        //Update
        public void Update(OntimePending ontimePending) {
            db.Entry(ontimePending).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            OntimePending ontimePending = db.OntimePendings.Find(deliveryNote);
            db.OntimePendings.Remove(ontimePending);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
