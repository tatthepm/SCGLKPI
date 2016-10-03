﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class InboundedFilesBs {
        private InboundedFilesDb objDb;
        public InboundedFilesBs() {
            objDb = new InboundedFilesDb();
        }
        //GetAll
        public IEnumerable<InboundedFiles> GetAll() {
            return objDb.GetAll();
        }
        //GetByFilter
        public IEnumerable<InboundedFiles> GetByShipment(string DeliveryNo)
        {
            return objDb.GetByShipment(DeliveryNo);
        }
        public InboundedFiles GetByID(string ID) {
            return objDb.GetByID(ID);
        }

        //Insert
        public void Insert(InboundedFiles InboundedFiles) {
            objDb.Insert(InboundedFiles);
        }

        //Update
        public void Update(InboundedFiles InboundedFiles) {
            objDb.Update(InboundedFiles);
        }

        //Delete
        public void Delete(string shipmentNo) {
            objDb.Delete(shipmentNo);
        }
    }
}
