using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OntimeInboundMonthBs {
        private OntimeInboundMonthDb objDb;
        public OntimeInboundMonthBs() {
            objDb = new OntimeInboundMonthDb();
        }
        //GetAll
        public IQueryable<OntimeInboundMonth> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeInboundMonth GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeInboundMonth ontimeInboundMonth) {
            objDb.Insert(ontimeInboundMonth);
        }

        //Update
        public void Update(OntimeInboundMonth ontimeInboundMonth) {
            objDb.Update(ontimeInboundMonth);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
