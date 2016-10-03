using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
   public class OutboundedFilesDb {
        private SCGLKPIDbContext db;
        public OutboundedFilesDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<OutboundedFiles> GetAll() {
            return db.OutboundedFiles.ToList();
        }
        //GetById
        public OutboundedFiles GetByID(int ID)
        {
            return db.OutboundedFiles.Find(ID);
        }
        /// <summary>
        /// Get records by shipment number
        /// </summary>
        /// <param name="DeliveryNo">DN number</param>
        /// <returns>IEnumerable of OutboundedFiles</returns>
        public IEnumerable<OutboundedFiles> GetByShipment(string DeliveryNo)
        {
            return db.OutboundedFiles.Where(x => x.DELVNO == DeliveryNo).Take(1000);
        }

        //Insert
        public void Insert(OutboundedFiles OutboundedFiles) {
            db.OutboundedFiles.Add(OutboundedFiles);
            Save();
        }

        //Update
        public void Update(OutboundedFiles OutboundedFiles) {
            db.Entry(OutboundedFiles).State = EntityState.Modified;
            Save();
        }

        /// <summary>
        /// Delete file record by ID
        /// </summary>
        /// <param name="ID">Record ID</param>
        public void Delete(int ID) {
            OutboundedFiles OutboundedFiles = db.OutboundedFiles.Find(ID);
            db.OutboundedFiles.Remove(OutboundedFiles);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
