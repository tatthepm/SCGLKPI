using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class TenderedDelayDb {
        private SCGLKPIDbContext db;
        public TenderedDelayDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<TenderedDelay> GetAll() {
            return db.TenderedDelays;
        }
        //GetByFilter
        public IQueryable<TenderedDelay> GetByFilter(string segment_id, int month, int year)
        {
            return db.TenderedDelays.Where(x => x.SEGMENT == segment_id && x.FTNRDDATE_D.Value.Year == year && x.FTNRDDATE_D.Value.Month == month);
        }
        //GetById
        public TenderedDelay GetByID(string shipmentNo) {
            return db.TenderedDelays.Find(shipmentNo);
        }
        //Insert
        public void Insert(TenderedDelay tenderedDelay) {
            db.TenderedDelays.Add(tenderedDelay);
            Save();
        }

        //Update
        public void Update(TenderedDelay tenderedDelay) {
            db.Entry(tenderedDelay).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string shipmentNo) {
            TenderedDelay tenderedDelay = db.TenderedDelays.Find(shipmentNo);
            db.TenderedDelays.Remove(tenderedDelay);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
