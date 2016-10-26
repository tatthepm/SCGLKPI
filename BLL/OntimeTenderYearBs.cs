using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class OntimeTenderYearBs {
        private OntimeTenderYearDb objDb;
        public OntimeTenderYearBs() {
            objDb = new OntimeTenderYearDb();
        }
        //GetAll
        public IQueryable<OntimeTenderYear> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public OntimeTenderYear GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(OntimeTenderYear ontimeTenderYear) {
            objDb.Insert(ontimeTenderYear);
        }

        //Update
        public void Update(OntimeTenderYear ontimeTenderYear) {
            objDb.Update(ontimeTenderYear);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
