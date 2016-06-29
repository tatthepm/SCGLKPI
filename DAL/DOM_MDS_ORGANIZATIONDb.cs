using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class DOM_MDS_ORGANIZATIONDb {
        private SCGLKPIDbContext db;
        public DOM_MDS_ORGANIZATIONDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<DOM_MDS_ORGANIZATION> GetAll() {
            return db.DOM_MDS_ORGANIZATIONs.ToList();
        }

        //GetById
        public DOM_MDS_ORGANIZATION GetByID(string MATFRIGRP, string SHPPOINT_ID, string REGION_ID, DateTime VALIDFROM) {
            return db.DOM_MDS_ORGANIZATIONs.Find(MATFRIGRP, SHPPOINT_ID, REGION_ID, VALIDFROM);
        }

        //Insert
        public void Insert(DOM_MDS_ORGANIZATION DOM_MDS_ORGANIZATION) {
            db.DOM_MDS_ORGANIZATIONs.Add(DOM_MDS_ORGANIZATION);
            Save();
        }

        //Update
        public void Update(DOM_MDS_ORGANIZATION DOM_MDS_ORGANIZATION) {
            db.Entry(DOM_MDS_ORGANIZATION).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string MATFRIGRP, string SHPPOINT_ID, string REGION_ID, DateTime VALIDFROM) {
            DOM_MDS_ORGANIZATION organization = db.DOM_MDS_ORGANIZATIONs.Find(MATFRIGRP, SHPPOINT_ID, REGION_ID, VALIDFROM);
            db.DOM_MDS_ORGANIZATIONs.Remove(organization);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
