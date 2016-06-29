using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
   public  class ReasonOutboundBs {
        private ReasonOutboundDb objDb;
        public ReasonOutboundBs() {
            objDb = new ReasonOutboundDb();
        }
        //GetAll
        public IEnumerable<ReasonOutbound> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public ReasonOutbound GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(ReasonOutbound reasonOutbound) {
            objDb.Insert(reasonOutbound);
        }

        //Update
        public void Update(ReasonOutbound reasonOutbound) {
            objDb.Update(reasonOutbound);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
