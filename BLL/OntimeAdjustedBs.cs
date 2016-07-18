using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;


namespace BLL {
    public class OntimeAdjustedBs {
        private OntimeAdjustedDb objDb;
        public OntimeAdjustedBs() {
            objDb = new OntimeAdjustedDb();
        }
        //GetAll
        public IEnumerable<OntimeAdjusted> GetAll() {
            return objDb.GetAll();
        }
        //GetByFilter
        public IEnumerable<OntimeAdjusted> GetByFilter(string department_id, string section_id, int month, int year)
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
        public OntimeAdjusted GetByID(string deliveryNote) {
            return objDb.GetByID(deliveryNote);
        }

        //Insert
        public void Insert(OntimeAdjusted ontimeAdjusted) {
            objDb.Insert(ontimeAdjusted);
        }

        //Update
        public void Update(OntimeAdjusted ontimeAdjusted) {
            objDb.Update(ontimeAdjusted);
        }

        //Delete
        public void Delete(string deliveryNote) {
            objDb.Delete(deliveryNote);
        }
    }
}
