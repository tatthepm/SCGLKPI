﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class DWH_ONTIME_DNDb {
        private SCGLKPIDbContext db;
        public DWH_ONTIME_DNDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<DWH_ONTIME_DN> GetAll() {
            return db.DWH_ONTIME_DNs.ToList();
        }

        //GetById
        public DWH_ONTIME_DN GetByID(string DELVNO) {
            return db.DWH_ONTIME_DNs.Find(DELVNO);
        }

        //Insert
        public void Insert(DWH_ONTIME_DN DWH_ONTIME_DN) {
            db.DWH_ONTIME_DNs.Add(DWH_ONTIME_DN);
            Save();
        }

        //Update
        public void Update(DWH_ONTIME_DN DWH_ONTIME_DN) {
            db.Entry(DWH_ONTIME_DN).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string DELVNO) {
            DWH_ONTIME_DN ontimeDN = db.DWH_ONTIME_DNs.Find(DELVNO);
            db.DWH_ONTIME_DNs.Remove(ontimeDN);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
