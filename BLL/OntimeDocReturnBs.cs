using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OntimeDocReturnBs {
        private OntimeDocReturnDb objDb;
        public OntimeDocReturnBs() {
            objDb = new OntimeDocReturnDb();
        }

        //GetAll
        public IEnumerable<OntimeDocReturn> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeDocReturn GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeDocReturn ontimeDocReturn) {
            objDb.Insert(ontimeDocReturn);
        }

        //Update
        public void Update(OntimeDocReturn ontimeDocReturn) {
            objDb.Update(ontimeDocReturn);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
