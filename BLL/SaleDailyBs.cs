using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL
{
    public class SaleDailyBs
    {
        private SaleDailyDb objDb;
        public SaleDailyBs()
        {
            objDb = new SaleDailyDb();
        }
        //GetAll
        public IQueryable<SaleSummaryDaily> GetAll()
        {
            return objDb.GetAll();
        }
        public IQueryable<SaleSummaryDaily> GetByDate(DateTime? FromDateSearch, DateTime? ToDateSearch)
        {
            return objDb.GetByDate(FromDateSearch, ToDateSearch);
        }
        public SaleSummaryDaily GetByID(int ID)
        {
            return objDb.GetByID(ID);
        }

        //Insert
        public void Insert(SaleSummaryDaily SaleSummaryDaily)
        {
            objDb.Insert(SaleSummaryDaily);
        }

        //Update
        public void Update(SaleSummaryDaily SaleSummaryDaily)
        {
            objDb.Update(SaleSummaryDaily);
        }

        //Delete
        public void Delete(int ID)
        {
            objDb.Delete(ID);
        }
    }
}
