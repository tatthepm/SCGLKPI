using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class DocReturnAdjustedBs {
        private DocReturnAdjustedDb objDb;
        public DocReturnAdjustedBs() {
            objDb = new DocReturnAdjustedDb();
        }
        //GetAll
        public IQueryable<DocReturnAdjusted> GetAll() {
            return objDb.GetAll();
        }
        //GetByFilter
        public IQueryable<DocReturnAdjusted> GetByFilter(string department_id, string section_id, int month, int year)
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
        public DocReturnAdjusted GetByID(string deliveryNote) {
            return objDb.GetByID(deliveryNote);
        }

        //Insert
        public void Insert(DocReturnAdjusted docReturnAdjusted) {
            objDb.Insert(docReturnAdjusted);
        }

        //Update
        public void Update(DocReturnAdjusted docReturnAdjusted) {
            objDb.Update(docReturnAdjusted);
        }

        //Delete
        public void Delete(string deliveryNote) {
            objDb.Delete(deliveryNote);
        }
    }
}
