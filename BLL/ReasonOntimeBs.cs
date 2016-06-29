using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;


namespace BLL {
    public class ReasonOntimeBs {
        private ReasonOntimeDb objDb;
        public ReasonOntimeBs() {
            objDb = new ReasonOntimeDb();
        }
        //GetAll
        public IEnumerable<ReasonOntime> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public ReasonOntime GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //Insert
        public void Insert(ReasonOntime reasonOntime) {
            objDb.Insert(reasonOntime);
        }

        //Update
        public void Update(ReasonOntime reasonOntime) {
            objDb.Update(reasonOntime);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
