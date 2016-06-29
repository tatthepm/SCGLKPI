using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OntimeDocReturnYearBs {
        private OntimeDocReturnYearDb objDb;
        public OntimeDocReturnYearBs() {
            objDb = new OntimeDocReturnYearDb();
        }
        //GetAll
        public IEnumerable<OntimeDocReturnYear> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeDocReturnYear GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeDocReturnYear ontimeDocReturnYear) {
            objDb.Insert(ontimeDocReturnYear);
        }

        //Update
        public void Update(OntimeDocReturnYear ontimeDocReturnYear) {
            objDb.Update(ontimeDocReturnYear);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
