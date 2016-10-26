using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OntimeFilesBs {
        private OntimeFilesDb objDb;
        public OntimeFilesBs() {
            objDb = new OntimeFilesDb();
        }
        //GetAll
        public IQueryable<OntimeFiles> GetAll() {
            return objDb.GetAll();
        }
        //GetByFilter
        public IQueryable<OntimeFiles> GetByShipment(string DeliveryNo)
        {
            return objDb.GetByShipment(DeliveryNo);
        }
        public OntimeFiles GetByID(int ID) {
            return objDb.GetByID(ID);
        }

        //Insert
        public void Insert(OntimeFiles OntimeFiles) {
            objDb.Insert(OntimeFiles);
        }

        //Update
        public void Update(OntimeFiles OntimeFiles) {
            objDb.Update(OntimeFiles);
        }

        //Delete
        public void Delete(int ID) {
            objDb.Delete(ID);
        }
    }
}
