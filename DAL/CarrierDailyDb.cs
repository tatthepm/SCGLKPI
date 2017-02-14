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
    public class CarrierDailyDb
    {
        private SCGLKPIDbContext db;
        public CarrierDailyDb()
        {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<CarrierSummaryDaily> GetByDate(DateTime? FromDateSearch, DateTime? ToDateSearch)
        {
            return db.CarrierSummaryDaily.Where(x => x.ActualGiDate >= FromDateSearch && x.ActualGiDate <= ToDateSearch);
        }

        //GetById
        public CarrierSummaryDaily GetByID(int Id)
        {
            return db.CarrierSummaryDaily.Find(Id);
        }

        //Insert
        public void Insert(CarrierSummaryDaily CarrierSummaryDaily)
        {
            db.CarrierSummaryDaily.Add(CarrierSummaryDaily);
            Save();
        }

        //Update
        public void Update(CarrierSummaryDaily CarrierSummaryDaily)
        {
            db.Entry(CarrierSummaryDaily).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id)
        {
            CarrierSummaryDaily CarrierSummaryDaily = db.CarrierSummaryDaily.Find(Id);
            db.CarrierSummaryDaily.Remove(CarrierSummaryDaily);
            Save();
        }

        //Save
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
