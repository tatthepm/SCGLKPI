using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class AcceptedFilesBs {
        private AcceptedFilesDb objDb;
        public AcceptedFilesBs() {
            objDb = new AcceptedFilesDb();
        }
        //GetAll
        public IQueryable<AcceptedFiles> GetAll() {
            return objDb.GetAll();
        }
        //GetByFilter
        public IQueryable<AcceptedFiles> GetByShipment(string shipment)
        {
            return objDb.GetByShipment(shipment);
        }
        public AcceptedFiles GetByID(int ID) {
            return objDb.GetByID(ID);
        }

        //Insert
        public void Insert(AcceptedFiles acceptedFiles) {
            objDb.Insert(acceptedFiles);
        }

        //Update
        public void Update(AcceptedFiles acceptedFiles) {
            objDb.Update(acceptedFiles);
        }

        //Delete
        public void Delete(int ID) {
            objDb.Delete(ID);
        }
    }
}
