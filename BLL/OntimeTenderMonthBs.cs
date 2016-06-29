using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;


namespace BLL {
    public class OntimeTenderMonthBs {
        private OntimeTenderMonthDb objDb;
        public OntimeTenderMonthBs() {
            objDb = new OntimeTenderMonthDb();
        }
        //GetAll
        public IEnumerable<OntimeTenderMonth> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeTenderMonth GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeTenderMonth ontimeTenderMonth) {
            objDb.Insert(ontimeTenderMonth);
        }

        //Update
        public void Update(OntimeTenderMonth ontimeTenderMonth) {
            objDb.Update(ontimeTenderMonth);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
