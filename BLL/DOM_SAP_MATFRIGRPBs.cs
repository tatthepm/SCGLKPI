using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;


namespace BLL {
   public class DOM_SAP_MATFRIGRPBs {
        private DOM_SAP_MATFRIGRPDb objDb;
        public DOM_SAP_MATFRIGRPBs() {
            objDb = new DOM_SAP_MATFRIGRPDb();
        }

        //GetAll
        public IQueryable<DOM_SAP_MATFRIGRP> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public DOM_SAP_MATFRIGRP GetByID(string MATFRIGRP) {
            return objDb.GetByID(MATFRIGRP);
        }

        //Insert
        public void Insert(DOM_SAP_MATFRIGRP DOM_SAP_MATFRIGRP) {
            objDb.Insert(DOM_SAP_MATFRIGRP);
        }

        //Update
        public void Update(DOM_SAP_MATFRIGRP DOM_SAP_MATFRIGRP) {
            objDb.Update(DOM_SAP_MATFRIGRP);
        }

        //Delete
        public void Delete(string MATFRIGRP) {
            objDb.Delete(MATFRIGRP);
        }
    }
}
