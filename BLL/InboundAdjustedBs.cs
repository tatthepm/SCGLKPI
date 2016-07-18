using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class InboundAdjustedBs {
        private InboundAdjustedDb objDb;
        public InboundAdjustedBs() {
            objDb = new InboundAdjustedDb();
        }
        //GetAll
        public IEnumerable<InboundAdjusted> GetAll() {
            return objDb.GetAll();
        }
        //GetByFilter
        public IEnumerable<InboundAdjusted> GetByFilter(string department_id, string section_id, int month, int year)
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
        public InboundAdjusted GetByID(string deliveryNote) {
            return objDb.GetByID(deliveryNote);
        }

        //Insert
        public void Insert(InboundAdjusted inboundAdjusted) {
            objDb.Insert(inboundAdjusted);
        }

        //Update
        public void Update(InboundAdjusted inboundAdjusted) {
            objDb.Update(inboundAdjusted);
        }

        //Delete
        public void Delete(string deliveryNote) {
            objDb.Delete(deliveryNote);
        }
    }
}
