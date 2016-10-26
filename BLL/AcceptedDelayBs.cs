using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class AcceptedDelayBs {
        private AcceptedDelayDb objDb;
        public AcceptedDelayBs() {
            objDb = new AcceptedDelayDb();
        }
        //GetAll
        public IQueryable<AcceptedDelay> GetAll() {
            return objDb.GetAll();
        }
        //GetByFilter
        public IQueryable<AcceptedDelay> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return objDb.GetByFilter(department_id, section_id, month, year);
        }
        //GetByMatName
        public IQueryable<BOLDropdownLists> GetByMatName()
        {
            return objDb.GetByMatName();
        }
        //GetByMatName (Overload)
        public IQueryable<BOLDropdownLists> GetByMatName(string departmentId, string sectionId)
        {
            return objDb.GetByMatName(departmentId, sectionId);
        }
        //GetBySection
        public IQueryable<BOLDropdownLists> GetBySection()
        {
            return objDb.GetBySection();
        }
        //GetBySection (Overload)
        public IQueryable<BOLDropdownLists> GetBySection(string departmentId)
        {
            return objDb.GetBySection(departmentId);
        }
        //GetById
        public AcceptedDelay GetByID(string shipmentNo) {
            return objDb.GetByID(shipmentNo);
        }

        //Insert
        public void Insert(AcceptedDelay acceptedDelay) {
            objDb.Insert(acceptedDelay);
        }

        //Update
        public void Update(AcceptedDelay acceptedDelay) {
            objDb.Update(acceptedDelay);
        }

        //Delete
        public void Delete(string shipmentNo) {
            objDb.Delete(shipmentNo);
        }
    }
}
