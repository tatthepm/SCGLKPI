using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
   public class AcceptedFilesDb {
        private SCGLKPIDbContext db;
        public AcceptedFilesDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<AcceptedFiles> GetAll() {
            return db.AcceptedFiles;
        }
        //GetById
        public AcceptedFiles GetByID(int ID)
        {
            return db.AcceptedFiles.Find(ID);
        }
        /// <summary>
        /// Get records by shipment number
        /// </summary>
        /// <param name="shipmentNo">shipment number</param>
        /// <returns>IQueryable of AcceptedFiles</returns>
        public IQueryable<AcceptedFiles> GetByShipment(string shipmentNo)
        {
            return db.AcceptedFiles.Where(x => x.SHPMNTNO == shipmentNo);
        }

        //Insert
        public void Insert(AcceptedFiles acceptedFiles) {
            db.AcceptedFiles.Add(acceptedFiles);
            Save();
        }

        //Update
        public void Update(AcceptedFiles acceptedFiles) {
            db.Entry(acceptedFiles).State = EntityState.Modified;
            Save();
        }

        /// <summary>
        /// Delete file record by ID
        /// </summary>
        /// <param name="ID">Record ID</param>
        public void Delete(int ID) {
            AcceptedFiles acceptedFiles = db.AcceptedFiles.Find(ID);
            db.AcceptedFiles.Remove(acceptedFiles);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
