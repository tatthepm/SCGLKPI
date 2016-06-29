using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OntimeOutboundMonthBs {
        private OntimeOutboundMonthDb objDb;
        public OntimeOutboundMonthBs() {
            objDb = new OntimeOutboundMonthDb();
        }
        //GetAll
        public IEnumerable<OntimeOutboundMonth> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeOutboundMonth GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeOutboundMonth ontimeOutboundMonth) {
            objDb.Insert(ontimeOutboundMonth);
        }

        //Update
        public void Update(OntimeOutboundMonth ontimeOutboundMonth) {
            objDb.Update(ontimeOutboundMonth);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
