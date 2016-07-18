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
        public IEnumerable<AcceptedDelay> GetAll() {
            return objDb.GetAll();
        }
        //GetByFilter
        public IEnumerable<AcceptedDelay> GetByFilter(string department_id, string section_id, int month, int year)
        {
            return objDb.GetByFilter(department_id, section_id, month, year);
        }
        //GetByMatName
        public IEnumerable<BOLDropdownLists> GetByMatName()
        {
            return objDb.GetByMatName();
        }
        //GetByMatName (Overload)
        public IEnumerable<BOLDropdownLists> GetByMatName(string departmentId, string sectionId)
        {
            return objDb.GetByMatName(departmentId, sectionId);
        }
        //GetBySection
        public IEnumerable<BOLDropdownLists> GetBySection()
        {
            return objDb.GetBySection();
        }
        //GetBySection (Overload)
        public IEnumerable<BOLDropdownLists> GetBySection(string departmentId)
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
