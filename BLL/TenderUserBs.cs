using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class TenderUserBs {
        private TenderUserDb objDb;
        public TenderUserBs() {
            objDb = new TenderUserDb();
        }
        //GetAll
        public IQueryable<TenderUsers> GetAll() {
            return objDb.GetAll();
        }
        //GetByFilter
        public IQueryable<TenderUsers> GetByFilter(string month, string year)
        {
            return objDb.GetByFilter(month, year);
        }
        //GetById
        public TenderUsers GetByID(string shipmentNo) {
            return objDb.GetByID(shipmentNo);
        }

        //Insert
        public void Insert(TenderUsers TenderUser) {
            objDb.Insert(TenderUser);
        }

        //Update
        public void Update(TenderUsers TenderUser) {
            objDb.Update(TenderUser);
        }

        //Delete
        public void Delete(string shipmentNo) {
            objDb.Delete(shipmentNo);
        }
    }
}
