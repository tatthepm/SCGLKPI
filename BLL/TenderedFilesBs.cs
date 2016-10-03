﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class TenderedFilesBs {
        private TenderedFilesDb objDb;
        public TenderedFilesBs() {
            objDb = new TenderedFilesDb();
        }
        //GetAll
        public IEnumerable<TenderedFiles> GetAll() {
            return objDb.GetAll();
        }
        //GetByFilter
        public IEnumerable<TenderedFiles> GetByShipment(string shipment)
        {
            return objDb.GetByShipment(shipment);
        }
        public TenderedFiles GetByID(string ID) {
            return objDb.GetByID(ID);
        }

        //Insert
        public void Insert(TenderedFiles TenderedFiles) {
            objDb.Insert(TenderedFiles);
        }

        //Update
        public void Update(TenderedFiles TenderedFiles) {
            objDb.Update(TenderedFiles);
        }

        //Delete
        public void Delete(string shipmentNo) {
            objDb.Delete(shipmentNo);
        }
    }
}
