using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class DocReturnDelayBs {
        private DocReturnDelayDb objDb;
        public DocReturnDelayBs() {
            objDb = new DocReturnDelayDb();
        }
        //GetAll
        public IQueryable<DocReturnDelay> GetAll() {
            return objDb.GetAll();
        }
        //GetByFilter
        public IQueryable<DocReturnDelay> GetByFilter(string department_id, string section_id, int month, int year)
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
        public DocReturnDelay GetByID(string deliveryNote) {
            return objDb.GetByID(deliveryNote);
        }

        //Insert
        public void Insert(DocReturnDelay docReturnDelay) {
            objDb.Insert(docReturnDelay);
        }

        //Update
        public void Update(DocReturnDelay docReturnDelay) {
            objDb.Update(docReturnDelay);
        }

        //Delete
        public void Delete(string deliveryNote) {
            objDb.Delete(deliveryNote);
        }
    }
}
