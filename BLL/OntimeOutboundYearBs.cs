using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
   public class OntimeOutboundYearBs {
        private OntimeOutboundYearDb objDb;
        public OntimeOutboundYearBs() {
            objDb = new OntimeOutboundYearDb();
        }
        //GetAll
        public IEnumerable<OntimeOutboundYear> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeOutboundYear GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeOutboundYear ontimeOutboundYear) {
            objDb.Insert(ontimeOutboundYear);
        }

        //Update
        public void Update(OntimeOutboundYear ontimeOutboundYear) {
            objDb.Update(ontimeOutboundYear);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
