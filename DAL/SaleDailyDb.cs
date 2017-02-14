using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using BOL;

namespace DAL
{
    public class SaleDailyDb
    {
        private SCGLKPIDbContext db;
        public SaleDailyDb()
        {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<SaleSummaryDaily> GetByDate(DateTime? FromDateSearch, DateTime? ToDateSearch)
        {
            return db.SaleSummaryDaily.Where(x => x.ActualGiDate >= FromDateSearch && x.ActualGiDate <= ToDateSearch);
        }

        //GetById
        public SaleSummaryDaily GetByID(int Id)
        {
            return db.SaleSummaryDaily.Find(Id);
        }

        //Insert
        public void Insert(SaleSummaryDaily SaleSummaryDaily)
        {
            db.SaleSummaryDaily.Add(SaleSummaryDaily);
            Save();
        }

        //Update
        public void Update(SaleSummaryDaily SaleSummaryDaily)
        {
            db.Entry(SaleSummaryDaily).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id)
        {
            SaleSummaryDaily SaleSummaryDaily = db.SaleSummaryDaily.Find(Id);
            db.SaleSummaryDaily.Remove(SaleSummaryDaily);
            Save();
        }

        //Save
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
