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
        public IEnumerable<DocReturnPending> GetAll() {
            return db.DocReturnPendings.ToList();
        }
        //GetByFilter
        public IEnumerable<DocReturnPending> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.DocReturnPendings.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.PLNDOCRETDATE_SCGL_D.Value.Year == year && x.PLNDOCRETDATE_SCGL_D.Value.Month == month);
        }
        //GetById
        public DocReturnPending GetByID(string deliveryNote)
        {
            return db.DocReturnPendings.Find(deliveryNote);
        }
        //GetByMatName
        public IEnumerable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.DocReturnPendings
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
            var Queryable = (from m in db.DocReturnPendings
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
            var Queryable = (from m in db.DocReturnPendings
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
            var Queryable = (from m in db.DocReturnPendings
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct().ToList();
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
