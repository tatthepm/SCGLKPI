using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OntimeOutboundBs {
        private OntimeOutboundDb objDb;
        public OntimeOutboundBs() {
            objDb = new OntimeOutboundDb();
        }

        //GetAll
        public IEnumerable<OntimeOutbound> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeOutbound GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeOutbound ontimeOutbound) {
            objDb.Insert(ontimeOutbound);
        }

        //Update
        public void Update(OntimeOutbound ontimeOutbound) {
            objDb.Update(ontimeOutbound);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
