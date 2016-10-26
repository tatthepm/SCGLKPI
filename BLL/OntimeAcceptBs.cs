using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OntimeAcceptBs {
        private OntimeAcceptDb objDb;
        public OntimeAcceptBs() {
            objDb = new OntimeAcceptDb();
        }
        //GetAll
        public IQueryable<OntimeAccept> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeAccept GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeAccept ontimeAccept) {
            objDb.Insert(ontimeAccept);
        }

        //Update
        public void Update(OntimeAccept ontimeAccept) {
            objDb.Update(ontimeAccept);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
