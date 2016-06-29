using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class DOM_MDS_ORGANIZATIONBs {
        private DOM_MDS_ORGANIZATIONDb objDb;
        public DOM_MDS_ORGANIZATIONBs() {
            objDb = new DOM_MDS_ORGANIZATIONDb();
        }

        //GetAll
        public IEnumerable<DOM_MDS_ORGANIZATION> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public DOM_MDS_ORGANIZATION GetByID(string MATFRIGRP, string SHPPOINT_ID, string REGION_ID, DateTime VALIDFROM) {
            return objDb.GetByID(MATFRIGRP, SHPPOINT_ID, REGION_ID, VALIDFROM);
        }

        //Insert
        public void Insert(DOM_MDS_ORGANIZATION DOM_MDS_ORGANIZATION) {
            objDb.Insert(DOM_MDS_ORGANIZATION);
        }

        //Update
        public void Update(DOM_MDS_ORGANIZATION DOM_MDS_ORGANIZATION) {
            objDb.Update(DOM_MDS_ORGANIZATION);
        }

        //Delete
        public void Delete(string MATFRIGRP, string SHPPOINT_ID, string REGION_ID, DateTime VALIDFROM) {
            objDb.Delete(MATFRIGRP, SHPPOINT_ID, REGION_ID, VALIDFROM);
        }
    }
}
