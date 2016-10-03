using System;
using System;
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
        public IEnumerable<DocReturnFiles> GetAll() {
            return db.DocReturnFiles.ToList();
        }
        //GetById
        public DocReturnFiles GetByID(string ID)
        {
            return db.DocReturnFiles.Find(ID);
        }
        /// <summary>
        /// Get records by shipment number
        /// </summary>
        /// <param name="DeliveryNo">DN number</param>
        /// <returns>IEnumerable of DocReturnFiles</returns>
        public IEnumerable<DocReturnFiles> GetByShipment(string DeliveryNo)
        {
            return db.DocReturnFiles.Where(x => x.DELVNO == DeliveryNo).Take(1000);
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
        public void Delete(string ID) {
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
