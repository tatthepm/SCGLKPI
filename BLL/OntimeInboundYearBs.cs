using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OntimeInboundYearBs {
        private OntimeInboundYearDb objDb;
        public OntimeInboundYearBs() {
            objDb = new OntimeInboundYearDb();
        }
        //GetAll
        public IEnumerable<OntimeInboundYear> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeInboundYear GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeInboundYear ontimeInboundYear) {
            objDb.Insert(ontimeInboundYear);
        }

        //Update
        public void Update(OntimeInboundYear ontimeInboundYear) {
            objDb.Update(ontimeInboundYear);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
