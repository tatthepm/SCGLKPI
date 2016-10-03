using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL
{
    public class TenderedFilesDb
    {
        private SCGLKPIDbContext db;
        public TenderedFilesDb()
        {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<TenderedFiles> GetAll()
        {
            return db.TenderedFiles.ToList();
        }
        //GetById
        public TenderedFiles GetByID(int ID)
        {
            return db.TenderedFiles.Find(ID);
        }
        /// <summary>
        /// Get records by shipment number
        /// </summary>
        /// <param name="shipmentNo">shipment number</param>
        /// <returns>IEnumerable of TenderedFiles</returns>
        public IEnumerable<TenderedFiles> GetByShipment(string shipmentNo)
        {
            return db.TenderedFiles.Where(x => x.SHPMNTNO == shipmentNo).Take(1000);
        }

        //Insert
        public void Insert(TenderedFiles TenderedFiles)
        {
            db.TenderedFiles.Add(TenderedFiles);
            Save();
        }

        //Update
        public void Update(TenderedFiles TenderedFiles)
        {
            db.Entry(TenderedFiles).State = EntityState.Modified;
            Save();
        }

        /// <summary>
        /// Delete file record by ID
        /// </summary>
        /// <param name="ID">Record ID</param>
        public void Delete(int ID)
        {
            TenderedFiles TenderedFiles = db.TenderedFiles.Find(ID);
            db.TenderedFiles.Remove(TenderedFiles);
            Save();
        }

        //Save
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
