using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class DOM_SAP_MATFRIGRPDb {
        private SCGLKPIDbContext db;
        public DOM_SAP_MATFRIGRPDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<DOM_SAP_MATFRIGRP> GetAll() {
            return db.DOM_SAP_MATFRIGRPs;
        }

        //GetById
        public DOM_SAP_MATFRIGRP GetByID(string MATFRIGRP) {
            return db.DOM_SAP_MATFRIGRPs.Find(MATFRIGRP);
        }

        //Insert
        public void Insert(DOM_SAP_MATFRIGRP DOM_SAP_MATFRIGRP) {
            db.DOM_SAP_MATFRIGRPs.Add(DOM_SAP_MATFRIGRP);
            Save();
        }

        //Update
        public void Update(DOM_SAP_MATFRIGRP DOM_SAP_MATFRIGRP) {
            db.Entry(DOM_SAP_MATFRIGRP).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string MATFRIGRP) {
            DOM_SAP_MATFRIGRP domSapMatFriGrp = db.DOM_SAP_MATFRIGRPs.Find(MATFRIGRP);
            db.DOM_SAP_MATFRIGRPs.Remove(domSapMatFriGrp);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
