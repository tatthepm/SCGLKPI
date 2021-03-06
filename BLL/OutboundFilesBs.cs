﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OutboundFilesBs {
        private OutboundedFilesDb objDb;
        public OutboundFilesBs() {
            objDb = new OutboundedFilesDb();
        }
        //GetAll
        public IQueryable<OutboundedFiles> GetAll() {
            return objDb.GetAll();
        }
        //GetByFilter
        public IQueryable<OutboundedFiles> GetByShipment(string DeliveryNo)
        {
            return objDb.GetByShipment(DeliveryNo);
        }
        public OutboundedFiles GetByID(int ID) {
            return objDb.GetByID(ID);
        }

        //Insert
        public void Insert(OutboundedFiles OutboundedFiles) {
            objDb.Insert(OutboundedFiles);
        }

        //Update
        public void Update(OutboundedFiles OutboundedFiles) {
            objDb.Update(OutboundedFiles);
        }

        //Delete
        public void Delete(int ID) {
            objDb.Delete(ID);
        }
    }
}
