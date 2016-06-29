using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class DWH_ONTIME_SHIPMENTDb {
        private SCGLKPIDbContext db;
        public DWH_ONTIME_SHIPMENTDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<DWH_ONTIME_SHIPMENT> GetAll() {
            return db.DWH_ONTIME_SHIPMENTs.ToList();
        }

        //GetById
        public DWH_ONTIME_SHIPMENT GetByID(string SHPMNTNO) {
            return db.DWH_ONTIME_SHIPMENTs.Find(SHPMNTNO);
        }

        //Insert
        public void Insert(DWH_ONTIME_SHIPMENT DWH_ONTIME_SHIPMENT) {
            db.DWH_ONTIME_SHIPMENTs.Add(DWH_ONTIME_SHIPMENT);
            Save();
        }

        //Update
        public void Update(DWH_ONTIME_SHIPMENT DWH_ONTIME_SHIPMENT) {
            db.Entry(DWH_ONTIME_SHIPMENT).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string SHPMNTNO) {
            DWH_ONTIME_SHIPMENT ontimeShipment = db.DWH_ONTIME_SHIPMENTs.Find(SHPMNTNO);
            db.DWH_ONTIME_SHIPMENTs.Remove(ontimeShipment);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
