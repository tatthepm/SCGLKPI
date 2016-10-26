using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class ReasonDocReturnBs {
        private ReasonDocReturnDb objDb;
        public ReasonDocReturnBs() {
            objDb = new ReasonDocReturnDb();
        }
        //GetAll
        public IQueryable<ReasonDocReturn> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public ReasonDocReturn GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(ReasonDocReturn reasonDocReturn) {
            objDb.Insert(reasonDocReturn);
        }

        //Update
        public void Update(ReasonDocReturn reasonDocReturn) {
            objDb.Update(reasonDocReturn);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
