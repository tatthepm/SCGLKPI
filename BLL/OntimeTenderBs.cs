using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OntimeTenderBs {

        private OntimeTenderDb objDb;
        public OntimeTenderBs() {
            objDb = new OntimeTenderDb();
        }

        //GetAll
        public IQueryable<OntimeTender> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeTender GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeTender ontimeTender) {
            objDb.Insert(ontimeTender);
        }

        //Update
        public void Update(OntimeTender ontimeTender) {
            objDb.Update(ontimeTender);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
