using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class ReasonAcceptedBs {
        private ReasonAcceptedDb objDb;
        public ReasonAcceptedBs() {
            objDb = new ReasonAcceptedDb();
        }
        //GetAll
        public IEnumerable<ReasonAccepted> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public ReasonAccepted GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(ReasonAccepted reasonAccepted) {
            objDb.Insert(reasonAccepted);
        }

        //Update
        public void Update(ReasonAccepted reasonAccepted) {
            objDb.Update(reasonAccepted);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
