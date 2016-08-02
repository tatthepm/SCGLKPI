﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class TenderedPendingDb {
        private SCGLKPIDbContext db;
        public TenderedPendingDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<TenderPending> GetAll() {
            return db.TenderPendings.ToList();
        }
        //GetByFilter
        public IEnumerable<TenderPending> GetByFilter(string segment_id, int month, int year)
        {
            return db.TenderPendings.Where(x => x.SEGMENT == segment_id && x.PLNTNRDDATE_D.Value.Year == year && x.PLNTNRDDATE_D.Value.Month == month).Take(1000);
        }
        //GetById
        public TenderPending GetByID(string shipmentNo) {
            return db.TenderPendings.Find(shipmentNo);
        }

        //Insert
        public void Insert(TenderPending tenderedPending) {
            db.TenderPendings.Add(tenderedPending);
            Save();
        }

        //Update
        public void Update(TenderPending tenderedPending) {
            db.Entry(tenderedPending).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string shipmentNo) {
            TenderPending tenderedPending = db.TenderPendings.Find(shipmentNo);
            db.TenderPendings.Remove(tenderedPending);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
