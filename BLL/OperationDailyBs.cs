using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL
{
    public class OperationDailyBs
    {
        private OperationDailyDb objDb;
        public OperationDailyBs()
        {
            objDb = new OperationDailyDb();
        }
        //GetAll
        public IQueryable<OperationSummaryDaily> GetByDate(DateTime? FromDateSearch, DateTime? ToDateSearch)
        {
            return objDb.GetByDate(FromDateSearch, ToDateSearch);
        }
        public OperationSummaryDaily GetByID(int ID)
        {
            return objDb.GetByID(ID);
        }

        //Insert
        public void Insert(OperationSummaryDaily OperationSummaryDaily)
        {
            objDb.Insert(OperationSummaryDaily);
        }

        //Update
        public void Update(OperationSummaryDaily OperationSummaryDaily)
        {
            objDb.Update(OperationSummaryDaily);
        }

        //Delete
        public void Delete(int ID)
        {
            objDb.Delete(ID);
        }
    }
}
