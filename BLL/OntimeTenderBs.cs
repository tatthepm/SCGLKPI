using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OntimeTenderBs {

        private OntimeTenderDb objDb;
        public OntimeTenderBs() {
            objDb = new OntimeTenderDb();
        }
        public IQueryable<BOLDropdownLists> GetByShipto(string segment)
        {
            return objDb.GetByShipto(segment);
        }
        //GetByShipPoint
        public IQueryable<BOLDropdownLists> GetByShipPoint(string segment)
        {
            return objDb.GetByShipPoint(segment);
        }
        //GetByTruckType
        public IQueryable<BOLDropdownLists> GetByTruckType(string segment)
        {
            return objDb.GetByTruckType(segment);
        }
        //GetAll
        public IQueryable<OntimeTender> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeTender GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeTender ontimeTender) {
            objDb.Insert(ontimeTender);
        }

        //Update
        public void Update(OntimeTender ontimeTender) {
            objDb.Update(ontimeTender);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
