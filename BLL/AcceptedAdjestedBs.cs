using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class AcceptedAdjustedBs {
        private AcceptedAdjustedDb objDb;
        public AcceptedAdjustedBs() {
            objDb = new AcceptedAdjustedDb();
        }
        //GetAll
        public IQueryable<AcceptedAdjusted> GetAll() {
            return objDb.GetAll();
        }
        public IQueryable<AcceptedAdjusted> GetByFilter(string department_id, string section_id, int month, int year)
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
        public AcceptedAdjusted GetByID(string shipmentNo) {
            return objDb.GetByID(shipmentNo);
        }

        //Insert
        public void Insert(AcceptedAdjusted acceptedAdjusted) {
            objDb.Insert(acceptedAdjusted);
        }

        //Update
        public void Update(AcceptedAdjusted acceptedAdjusted) {
            objDb.Update(acceptedAdjusted);
        }

        //Delete
        public void Delete(string shipmentNo) {
            objDb.Delete(shipmentNo);
        }
    }
}
