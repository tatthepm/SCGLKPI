using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;


namespace BLL {
   public class ReasonTenderedBs {
        private ReasonTenderedDb objDb;
        public ReasonTenderedBs() {
            objDb = new ReasonTenderedDb();
        }
        //GetAll
        public IQueryable<ReasonTendered> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public ReasonTendered GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(ReasonTendered ReasonTendered) {
            objDb.Insert(ReasonTendered);
        }

        //Update
        public void Update(ReasonTendered ReasonTendered) {
            objDb.Update(ReasonTendered);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
