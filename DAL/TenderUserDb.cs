using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class TenderUserDb {
        private SCGLKPIDbContext db;
        public TenderUserDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<TenderUsers> GetAll() {
            return db.TenderUsers;
        }
        //GetByFilter
        public IQueryable<TenderUsers> GetByFilter(string month, string year)
        {
            return db.TenderUsers.Where(x => x.Year == year && x.Month == month);
        }
        //GetById
        public TenderUsers GetByID(string shipmentNo) {
            return db.TenderUsers.Find(shipmentNo);
        }

        //Insert
        public void Insert(TenderUsers TenderUser) {
            db.TenderUsers.Add(TenderUser);
            Save();
        }

        //Update
        public void Update(TenderUsers TenderUser) {
            db.Entry(TenderUser).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string shipmentNo) {
            TenderUsers TenderUser = db.TenderUsers.Find(shipmentNo);
            db.TenderUsers.Remove(TenderUser);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
