using System;
using System;
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
        public IEnumerable<OntimeFiles> GetAll() {
            return db.OntimeFiles.ToList();
        }
        //GetById
        public OntimeFiles GetByID(string ID)
        {
            return db.OntimeFiles.Find(ID);
        }
        /// <summary>
        /// Get records by shipment number
        /// </summary>
        /// <param name="DeliveryNo">DN number</param>
        /// <returns>IEnumerable of OntimeFiles</returns>
        public IEnumerable<OntimeFiles> GetByShipment(string DeliveryNo)
        {
            return db.OntimeFiles.Where(x => x.DELVNO == DeliveryNo).Take(1000);
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
        public void Delete(string ID) {
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
