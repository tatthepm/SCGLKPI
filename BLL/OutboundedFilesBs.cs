using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OutboundedFilesBs {
        private OutboundedFilesDb objDb;
        public OutboundedFilesBs() {
            objDb = new OutboundedFilesDb();
        }
        //GetAll
        public IEnumerable<OutboundedFiles> GetAll() {
            return objDb.GetAll();
        }
        //GetByFilter
        public IEnumerable<OutboundedFiles> GetByShipment(string DeliveryNo)
        {
            return objDb.GetByShipment(DeliveryNo);
        }
        public OutboundedFiles GetByID(string ID) {
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
        public void Delete(string shipmentNo) {
            objDb.Delete(shipmentNo);
        }
    }
}
