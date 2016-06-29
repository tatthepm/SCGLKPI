using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
   public class OntimeDocReturnMonthBs {
        private OntimeDocReturnMonthDb objDb;
        public OntimeDocReturnMonthBs() {
            objDb = new OntimeDocReturnMonthDb();
        }
        //GetAll
        public IEnumerable<OntimeDocReturnMonth> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeDocReturnMonth GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeDocReturnMonth ontimeDocReturnMonth) {
            objDb.Insert(ontimeDocReturnMonth);
        }

        //Update
        public void Update(OntimeDocReturnMonth ontimeDocReturnMonth) {
            objDb.Update(ontimeDocReturnMonth);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
