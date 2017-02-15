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
    public class OperationDailyDb
    {
        private SCGLKPIDbContext db;
        public OperationDailyDb()
        {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<OperationSummaryDaily> GetAll()
        {
            return db.OperationSummaryDaily;
        }
        public IQueryable<OperationSummaryDaily> GetByDate(DateTime? FromDateSearch, DateTime? ToDateSearch)
        {
            return db.OperationSummaryDaily.Where(x => x.ActualGiDate >= FromDateSearch && x.ActualGiDate <= ToDateSearch);
        }

        //GetById
        public OperationSummaryDaily GetByID(int Id)
        {
            return db.OperationSummaryDaily.Find(Id);
        }

        //Insert
        public void Insert(OperationSummaryDaily OperationSummaryDaily)
        {
            db.OperationSummaryDaily.Add(OperationSummaryDaily);
            Save();
        }

        //Update
        public void Update(OperationSummaryDaily OperationSummaryDaily)
        {
            db.Entry(OperationSummaryDaily).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id)
        {
            OperationSummaryDaily OperationSummaryDaily = db.OperationSummaryDaily.Find(Id);
            db.OperationSummaryDaily.Remove(OperationSummaryDaily);
            Save();
        }

        //Save
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
