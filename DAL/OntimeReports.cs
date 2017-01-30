using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class OntimeReportsDb {
        private SCGLKPIDbContext db;
        public OntimeReportsDb() {
            db = new SCGLKPIDbContext();
        }

        //GetAll
        public IQueryable<OntimeReports> GetAll() {
            return db.OntimeReports;
        }

        //GetAll
        public IQueryable<OntimeReports> GetByFilter(DateTime FromDate, DateTime ToDate, string Department, string Section, string Segment, string SoldTo, string Carrier, string TruckType, string MatFriGrp)
        {
            var Queryable = from m in db.OntimeReports
                            where m.ActualGiDate >= FromDate && m.ActualGiDate <= ToDate
                            select m;
            if (Department != null)
                Queryable = Queryable.Where(x => x.DepartmentId == Department);
            if (Section != null)
                Queryable = Queryable.Where(x => x.SectionId == Section);
            if (Segment != null)
                Queryable = Queryable.Where(x => x.Segment == Segment);
            if (SoldTo != null)
                Queryable = Queryable.Where(x => x.SoldToId == SoldTo);
            if (Carrier != null)
                Queryable = Queryable.Where(x => x.CarrierId == Carrier);
            if (TruckType != null)
                Queryable = Queryable.Where(x => x.TruckType == TruckType);
            if (MatFriGrp != null)
                Queryable = Queryable.Where(x => x.MatFriGrp == MatFriGrp);
            return Queryable;
        }

        //GetBySection
        public IQueryable<BOLDropdownLists> GetBySection(string Department)
        {
            var Queryable = (from m in db.OntimeReports
                             where m.DepartmentId == Department
                             select new BOLDropdownLists
                             {
                                 Id = m.SectionId,
                                 Name = m.SectionName,
                             }).Distinct();
            return Queryable;
        }
        //GetBySegment
        public IQueryable<BOLDropdownLists> GetBySegment(string Department, string Section)
        {
            var Queryable = (from m in db.OntimeReports
                             where m.DepartmentId == Department && m.SectionId == Section
                             select new BOLDropdownLists
                             {
                                 Id = m.Segment,
                                 Name = m.Segment,
                             }).Distinct();
            return Queryable;
        }
        //GetByTruckType
        public IQueryable<BOLDropdownLists> GetByTruckType(string segment)
        {
            var Queryable = (from m in db.OntimeReports
                             where m.SubSegment == segment
                             select new BOLDropdownLists
                             {
                                 Id = m.TruckType,
                                 Name = m.TruckType,
                             }).Distinct();
            return Queryable;
        }
        //GetByMatName
        public IQueryable<BOLDropdownLists> GetByMatName(string segment)
        {
            var Queryable = (from m in db.OntimeReports
                             where m.SubSegment == segment
                             select new BOLDropdownLists
                             {
                                 Id = m.MatFriGrp,
                                 Name = m.MatName,
                             }).Distinct();
            return Queryable;
        }
        //GetByCarrier
        public IQueryable<BOLDropdownLists> GetByCarrier(string segment)
        {
            var Queryable = (from m in db.OntimeReports
                             where m.SubSegment == segment
                             select new BOLDropdownLists
                             {
                                 Id = m.CarrierId,
                                 Name = db.DOM_CMD_VENDORs.Where(q => q.VENDOR_CODE == m.CarrierId).Select(q => q.VENDOR_NAME).FirstOrDefault(),
                             }).Distinct();
            return Queryable;
        }

        //GetBySoldTo
        public IQueryable<BOLDropdownLists> GetBySoldTo(string segment)
        {
            var Queryable = (from m in db.OntimeReports
                             where m.SubSegment == segment
                             select new BOLDropdownLists
                             {
                                 Id = m.SoldToId,
                                 Name = m.SoldToName,
                             }).Distinct();
            return Queryable;
        }
        //GetById
        public OntimeReports GetByID(int Id) {
            return db.OntimeReports.Find(Id);
        }

        //Insert
        public void Insert(OntimeReports OntimeReports) {
            db.OntimeReports.Add(OntimeReports);
            Save();
        }

        //Update
        public void Update(OntimeReports OntimeReports) {
            db.Entry(OntimeReports).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            OntimeReports OntimeReports = db.OntimeReports.Find(Id);
            db.OntimeReports.Remove(OntimeReports);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
