using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OntimeReportsBs {

        private OntimeReportsDb objDb;
        public OntimeReportsBs() {
            objDb = new OntimeReportsDb();
        }
        /// <summary>
        /// Get Report Data by filtering
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="Department"></param>
        /// <param name="Section"></param>
        /// <param name="Segment"></param>
        /// <param name="SoldTo"></param>
        /// <param name="Carrier"></param>
        /// <param name="TruckType"></param>
        /// <param name="MatFriGrp"></param>
        /// <returns></returns>
        public IQueryable<OntimeReports> GetByFilter(DateTime FromDate, DateTime ToDate, string Department, string Section, string Segment, string SoldTo, string Carrier, string TruckType, string MatFriGrp)
        {
            return objDb.GetByFilter(FromDate,ToDate,Department,Section,Segment,SoldTo,Carrier,TruckType,MatFriGrp);
        }

        public IQueryable<BOLDropdownLists> GetBySection(string Department)
        {
            return objDb.GetBySection(Department);
        }
        /// <summary>
        /// Get Segment DropDown
        /// </summary>
        /// <param name="Department"></param>
        /// <param name="Section"></param>
        /// <returns></returns>
        public IQueryable<BOLDropdownLists> GetBySegment(string Department, string Section)
        {
            return objDb.GetBySegment(Department, Section);
        }
        /// <summary>
        /// Get Truck type Dropdown
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public IQueryable<BOLDropdownLists> GetByTruckType(string segment)
        {
            return objDb.GetByTruckType(segment);
        }
        /// <summary>
        /// Get MatName Dropdown
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public IQueryable<BOLDropdownLists> GetByMatName(string segment)
        {
            return objDb.GetByMatName(segment);
        }
        /// <summary>
        /// Get carrier Dropdown
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public IQueryable<BOLDropdownLists> GetByCarrier(string segment)
        {
            return objDb.GetByTruckType(segment);
        }
        /// <summary>
        /// Get Soldto Dropdown
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public IQueryable<BOLDropdownLists> GetBySoldTo(string segment)
        {
            return objDb.GetByTruckType(segment);
        }
        //GetAll
        public IQueryable<OntimeReports> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeReports GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeReports OntimeReports) {
            objDb.Insert(OntimeReports);
        }

        //Update
        public void Update(OntimeReports OntimeReports) {
            objDb.Update(OntimeReports);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
