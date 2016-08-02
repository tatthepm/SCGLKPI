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
        public IEnumerable<OntimeDelay> GetAll() {
            return db.OntimeDelays.ToList();
        }
        //GetByFilter
        public IEnumerable<OntimeDelay> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.OntimeDelays.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.ACDLVDATE_D.Value.Year == year && x.ACDLVDATE_D.Value.Month == month).Take(1000);
        }

        //GetById
        public OntimeDelay GetByID(string deliveryNote) {
            return db.OntimeDelays.Find(deliveryNote);
        }

        //GetByMatName
        public IEnumerable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.OntimeDelays
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
            var Queryable = (from m in db.OntimeDelays
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
            var Queryable = (from m in db.OntimeDelays
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
            var Queryable = (from m in db.OntimeDelays
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct().ToList();
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
