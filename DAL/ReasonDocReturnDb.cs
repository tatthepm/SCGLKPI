using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;


namespace DAL {
    public class ReasonDocReturnDb {
        private SCGLKPIDbContext db;
        public ReasonDocReturnDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<ReasonDocReturn> GetAll() {
            return db.ReasonDocReturns.ToList();
        }

        //GetById
        public ReasonDocReturn GetByID(int Id) {
            return db.ReasonDocReturns.Find(Id);
        }

        //Insert
        public void Insert(ReasonDocReturn reasonDocReturn) {
            db.ReasonDocReturns.Add(reasonDocReturn);
            Save();
        }

        //Update
        public void Update(ReasonDocReturn reasonDocReturn) {
            db.Entry(reasonDocReturn).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            ReasonDocReturn reasonDocReturn = db.ReasonDocReturns.Find(Id);
            db.ReasonDocReturns.Remove(reasonDocReturn);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
