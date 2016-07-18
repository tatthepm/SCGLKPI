using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
   public class AcceptedPendingDb {
        private SCGLKPIDbContext db;
        public AcceptedPendingDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<AcceptPending> GetAll() {
            return db.AcceptPendings.ToList();
        }
        //GetByFilter
        public IEnumerable<AcceptPending> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.AcceptPendings.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.PLNACPDDATE_D.Value.Year == year && x.PLNACPDDATE_D.Value.Month == month);
        }
        //GetById
        public AcceptPending GetByID(string shipmentNo)
        {
            return db.AcceptPendings.Find(shipmentNo);
        }
        //GetByMatName
        public IEnumerable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.AcceptPendings
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
            var Queryable = (from m in db.AcceptPendings
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
            var Queryable = (from m in db.AcceptPendings
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
            var Queryable = (from m in db.AcceptPendings
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct().ToList();
            return Queryable;
        }

        //Insert
        public void Insert(AcceptPending acceptedPending) {
            db.AcceptPendings.Add(acceptedPending);
            Save();
        }

        //Update
        public void Update(AcceptPending acceptedPending) {
            db.Entry(acceptedPending).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string shipmentNo) {
            AcceptPending acceptedPending = db.AcceptPendings.Find(shipmentNo);
            db.AcceptPendings.Remove(acceptedPending);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
