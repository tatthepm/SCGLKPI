using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class ReasonInboundBs {
        private ReasonInboundDb objDb;
        public ReasonInboundBs() {
            objDb = new ReasonInboundDb();
        }
        //GetAll
        public IEnumerable<ReasonInbound> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public ReasonInbound GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(ReasonInbound reasonInbound) {
            objDb.Insert(reasonInbound);
        }

        //Update
        public void Update(ReasonInbound reasonInbound) {
            objDb.Update(reasonInbound);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
