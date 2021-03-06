﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OutboundPendingDb {
        private SCGLKPIDbContext db;
        public OutboundPendingDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<OutboundPending> GetAll() {
            return db.OutboundPendings;
        }

        //GetByFilter
        public IQueryable<OutboundPending> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return db.OutboundPendings.Where(x => x.DEPARTMENT_ID == department_id && x.SECTION_ID == section_id && x.PLNOUTBDATE_D.Value.Year == year && x.PLNOUTBDATE_D.Value.Month == month);
        }
        //GetById
        public OutboundPending GetByID(string deliveryNote)
        {
            return db.OutboundPendings.Find(deliveryNote);
        }
        //GetByMatName
        public IQueryable<BOLDropdownLists> GetByMatName()
        {
            var Queryable = (from m in db.OutboundPendings
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
            var Queryable = (from m in db.OutboundPendings
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
            var Queryable = (from m in db.OutboundPendings
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
            var Queryable = (from m in db.OutboundPendings
                             where m.DEPARTMENT_ID == departmentId
                             select new BOLDropdownLists
                             {
                                 Id = m.SECTION_ID,
                                 Name = m.SECTION_NAME,
                             }).Distinct();
            return Queryable;
        }

        //Insert
        public void Insert(OutboundPending outboundPending) {
            db.OutboundPendings.Add(outboundPending);
            Save();
        }

        //Update
        public void Update(OutboundPending outboundPending) {
            db.Entry(outboundPending).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            OutboundPending outboundPending = db.OutboundPendings.Find(deliveryNote);
            db.OutboundPendings.Remove(outboundPending);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
