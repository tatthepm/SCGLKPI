﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
   public class DocReturnFilesDb {
        private SCGLKPIDbContext db;
        public DocReturnFilesDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<DocReturnFiles> GetAll() {
            return db.DocReturnFiles;
        }
        //GetById
        public DocReturnFiles GetByID(int ID)
        {
            return db.DocReturnFiles.Find(ID);
        }
        /// <summary>
        /// Get records by shipment number
        /// </summary>
        /// <param name="DeliveryNo">DN number</param>
        /// <returns>IQueryable of DocReturnFiles</returns>
        public IQueryable<DocReturnFiles> GetByShipment(string DeliveryNo)
        {
            return db.DocReturnFiles.Where(x => x.DELVNO == DeliveryNo);
        }

        //Insert
        public void Insert(DocReturnFiles DocReturnFiles) {
            db.DocReturnFiles.Add(DocReturnFiles);
            Save();
        }

        //Update
        public void Update(DocReturnFiles DocReturnFiles) {
            db.Entry(DocReturnFiles).State = EntityState.Modified;
            Save();
        }

        /// <summary>
        /// Delete file record by ID
        /// </summary>
        /// <param name="ID">Record ID</param>
        public void Delete(int ID) {
            DocReturnFiles DocReturnFiles = db.DocReturnFiles.Find(ID);
            db.DocReturnFiles.Remove(DocReturnFiles);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
