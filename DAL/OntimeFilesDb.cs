﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
   public class OntimeFilesDb {
        private SCGLKPIDbContext db;
        public OntimeFilesDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<OntimeFiles> GetAll() {
            return db.OntimeFiles;
        }
        //GetById
        public OntimeFiles GetByID(int ID)
        {
            return db.OntimeFiles.Find(ID);
        }
        /// <summary>
        /// Get records by shipment number
        /// </summary>
        /// <param name="DeliveryNo">DN number</param>
        /// <returns>IQueryable of OntimeFiles</returns>
        public IQueryable<OntimeFiles> GetByShipment(string DeliveryNo)
        {
            return db.OntimeFiles.Where(x => x.DELVNO == DeliveryNo);
        }

        //Insert
        public void Insert(OntimeFiles OntimeFiles) {
            db.OntimeFiles.Add(OntimeFiles);
            Save();
        }

        //Update
        public void Update(OntimeFiles OntimeFiles) {
            db.Entry(OntimeFiles).State = EntityState.Modified;
            Save();
        }

        /// <summary>
        /// Delete file record by ID
        /// </summary>
        /// <param name="ID">Record ID</param>
        public void Delete(int ID) {
            OntimeFiles OntimeFiles = db.OntimeFiles.Find(ID);
            db.OntimeFiles.Remove(OntimeFiles);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
