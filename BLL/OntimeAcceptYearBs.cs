using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
   public class OntimeAcceptYearBs {
        private OntimeAcceptYearDb objDb;
        public OntimeAcceptYearBs() {
            objDb = new OntimeAcceptYearDb();
        }
        //GetAll
        public IEnumerable<OntimeAcceptYear> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeAcceptYear GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeAcceptYear ontimeAcceptYear) {
            objDb.Insert(ontimeAcceptYear);
        }

        //Update
        public void Update(OntimeAcceptYear ontimeAcceptYear) {
            objDb.Update(ontimeAcceptYear);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
