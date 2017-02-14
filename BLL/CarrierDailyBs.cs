using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL
{
    public class CarrierDailyBs
    {
        private CarrierDailyDb objDb;
        public CarrierDailyBs()
        {
            objDb = new CarrierDailyDb();
        }
        //GetAll
        public IQueryable<CarrierSummaryDaily> GetByDate(DateTime? FromDateSearch, DateTime? ToDateSearch)
        {
            return objDb.GetByDate(FromDateSearch, ToDateSearch);
        }
        public CarrierSummaryDaily GetByID(int ID)
        {
            return objDb.GetByID(ID);
        }

        //Insert
        public void Insert(CarrierSummaryDaily CarrierSummaryDaily)
        {
            objDb.Insert(CarrierSummaryDaily);
        }

        //Update
        public void Update(CarrierSummaryDaily CarrierSummaryDaily)
        {
            objDb.Update(CarrierSummaryDaily);
        }

        //Delete
        public void Delete(int ID)
        {
            objDb.Delete(ID);
        }
    }
}
