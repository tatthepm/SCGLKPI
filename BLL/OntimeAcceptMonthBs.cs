using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OntimeAcceptMonthBs {
        private OntimeAcceptMonthDb objDb;
        public OntimeAcceptMonthBs() {
            objDb = new OntimeAcceptMonthDb();
        }
        //GetAll
        public IEnumerable<OntimeAcceptMonth> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeAcceptMonth GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeAcceptMonth ontimeAcceptMonth) {
            objDb.Insert(ontimeAcceptMonth);
        }

        //Update
        public void Update(OntimeAcceptMonth ontimeAcceptMonth) {
            objDb.Update(ontimeAcceptMonth);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
