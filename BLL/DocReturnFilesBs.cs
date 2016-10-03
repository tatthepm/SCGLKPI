using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class DocReturnFilesBs {
        private DocReturnFilesDb objDb;
        public DocReturnFilesBs() {
            objDb = new DocReturnFilesDb();
        }
        //GetAll
        public IEnumerable<DocReturnFiles> GetAll() {
            return objDb.GetAll();
        }
        //GetByFilter
        public IEnumerable<DocReturnFiles> GetByShipment(string DeliveryNo)
        {
            return objDb.GetByShipment(DeliveryNo);
        }
        public DocReturnFiles GetByID(string ID) {
            return objDb.GetByID(ID);
        }

        //Insert
        public void Insert(DocReturnFiles DocReturnFiles) {
            objDb.Insert(DocReturnFiles);
        }

        //Update
        public void Update(DocReturnFiles DocReturnFiles) {
            objDb.Update(DocReturnFiles);
        }

        //Delete
        public void Delete(string shipmentNo) {
            objDb.Delete(shipmentNo);
        }
    }
}
