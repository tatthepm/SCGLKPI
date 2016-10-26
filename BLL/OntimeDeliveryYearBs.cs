using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;


namespace BLL {
    public class OntimeDeliveryYearBs {
        private OntimeDeliveryYearDb objDb;
        public OntimeDeliveryYearBs() {
            objDb = new OntimeDeliveryYearDb();
        }
        //GetAll
        public IQueryable<OntimeDeliveryYear> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeDeliveryYear GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeDeliveryYear ontimeDeliveryYear) {
            objDb.Insert(ontimeDeliveryYear);
        }

        //Update
        public void Update(OntimeDeliveryYear ontimeDeliveryYear) {
            objDb.Update(ontimeDeliveryYear);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
