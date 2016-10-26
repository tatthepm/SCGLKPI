using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
  public  class DOM_CMD_VENDORDb {
        private SCGLKPIDbContext db;
        public DOM_CMD_VENDORDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<DOM_CMD_VENDOR> GetAll() {
            return db.DOM_CMD_VENDORs;
        }

        //GetById
        public DOM_CMD_VENDOR GetByID(string VENDOR_CODE) {
            return db.DOM_CMD_VENDORs.Find(VENDOR_CODE);
        }

        //Insert
        public void Insert(DOM_CMD_VENDOR DOM_CMD_VENDOR) {
            db.DOM_CMD_VENDORs.Add(DOM_CMD_VENDOR);
            Save();
        }

        //Update
        public void Update(DOM_CMD_VENDOR DOM_CMD_VENDOR) {
            db.Entry(DOM_CMD_VENDOR).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string VENDOR_CODE) {
            DOM_CMD_VENDOR vendor = db.DOM_CMD_VENDORs.Find(VENDOR_CODE);
            db.DOM_CMD_VENDORs.Remove(vendor);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
