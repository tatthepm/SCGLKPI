using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
   public class OntimeInboundBs {
        private OntimeInboundDb objDb;
        public OntimeInboundBs() {
            objDb = new OntimeInboundDb();
        }

        //GetAll
        public IQueryable<OntimeInbound> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeInbound GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeInbound ontimeInbound) {
            objDb.Insert(ontimeInbound);
        }

        //Update
        public void Update(OntimeInbound ontimeInbound) {
            objDb.Update(ontimeInbound);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
