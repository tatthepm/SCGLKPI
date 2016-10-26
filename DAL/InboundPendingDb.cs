using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class InboundPendingDb {
        private SCGLKPIDbContext db;
        public InboundPendingDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<InboundPending> GetAll() {
            return db.InboundPendings;
        }

        //GetByFilter
        public IQueryable<InboundPending> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.InboundPendings.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.PLNINBDATE_D.Value.Year == year && x.PLNINBDATE_D.Value.Month == month).Take(1000);
        }

        //GetById
        public InboundPending GetByID(string deliveryNote)
        {
            return db.InboundPendings.Find(deliveryNote);
        }
        //GetByMatName
        public IQueryable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.InboundPendings
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
            var Queryable = (from m in db.InboundPendings
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
            var Queryable = (from m in db.InboundPendings
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
            var Queryable = (from m in db.InboundPendings
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct();
            return Queryable;
        }

        //Insert
        public void Insert(InboundPending inboundPending) {
            db.InboundPendings.Add(inboundPending);
            Save();
        }

        //Update
        public void Update(InboundPending inboundPending) {
            db.Entry(inboundPending).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            InboundPending inboundPending = db.InboundPendings.Find(deliveryNote);
            db.InboundPendings.Remove(inboundPending);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
