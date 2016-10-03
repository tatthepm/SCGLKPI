using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
   public class InboundedFilesDb {
        private SCGLKPIDbContext db;
        public InboundedFilesDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<InboundedFiles> GetAll() {
            return db.InboundedFiles.ToList();
        }
        //GetById
        public InboundedFiles GetByID(string ID)
        {
            return db.InboundedFiles.Find(ID);
        }
        /// <summary>
        /// Get records by shipment number
        /// </summary>
        /// <param name="DeliveryNo">DN number</param>
        /// <returns>IEnumerable of InboundedFiles</returns>
        public IEnumerable<InboundedFiles> GetByShipment(string DeliveryNo)
        {
            return db.InboundedFiles.Where(x => x.DELVNO == DeliveryNo).Take(1000);
        }

        //Insert
        public void Insert(InboundedFiles InboundedFiles) {
            db.InboundedFiles.Add(InboundedFiles);
            Save();
        }

        //Update
        public void Update(InboundedFiles InboundedFiles) {
            db.Entry(InboundedFiles).State = EntityState.Modified;
            Save();
        }

        /// <summary>
        /// Delete file record by ID
        /// </summary>
        /// <param name="ID">Record ID</param>
        public void Delete(string ID) {
            InboundedFiles InboundedFiles = db.InboundedFiles.Find(ID);
            db.InboundedFiles.Remove(InboundedFiles);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
