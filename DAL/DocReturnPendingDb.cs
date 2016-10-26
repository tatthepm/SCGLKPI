using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class DocReturnPendingDb {
        private SCGLKPIDbContext db;
        public DocReturnPendingDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<DocReturnPending> GetAll() {
            return db.DocReturnPendings;
        }
        //GetByFilter
        public IQueryable<DocReturnPending> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.DocReturnPendings.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.PLNDOCRETDATE_SCGL_D.Value.Year == year && x.PLNDOCRETDATE_SCGL_D.Value.Month == month).Take(1000);
        }
        //GetById
        public DocReturnPending GetByID(string deliveryNote)
        {
            return db.DocReturnPendings.Find(deliveryNote);
        }
        //GetByMatName
        public IQueryable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.DocReturnPendings
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
            var Queryable = (from m in db.DocReturnPendings
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
            var Queryable = (from m in db.DocReturnPendings
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
            var Queryable = (from m in db.DocReturnPendings
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct();
            return Queryable;
        }

        //Insert
        public void Insert(DocReturnPending outboundPending) {
            db.DocReturnPendings.Add(outboundPending);
            Save();
        }

        //Update
        public void Update(DocReturnPending docReturnPending) {
            db.Entry(docReturnPending).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            DocReturnPending docReturnPending = db.DocReturnPendings.Find(deliveryNote);
            db.DocReturnPendings.Remove(docReturnPending);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
