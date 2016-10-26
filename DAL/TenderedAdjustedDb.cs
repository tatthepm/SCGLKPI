using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class TenderedAdjustedDb {
        private SCGLKPIDbContext db;
        public TenderedAdjustedDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<TenderedAdjusted> GetAll() {
            return db.TenderedAdjusted;
        }

        //GetByFilter
        public IQueryable<TenderedAdjusted> GetByFilter(string segment_id, int month, int year)
        {
            return db.TenderedAdjusted.Where(x => x.SEGMENT == segment_id && x.FTNRDDATE_D.Value.Year == year && x.FTNRDDATE_D.Value.Month == month).Take(1000);
        }

        //GetById
        public TenderedAdjusted GetByID(string shipmentNo) {
            return db.TenderedAdjusted.Find(shipmentNo);
        }

        //Insert
        public void Insert(TenderedAdjusted tenderedAdjusted) {
            db.TenderedAdjusted.Add(tenderedAdjusted);
            Save();
        }

        //Update
        public void Update(TenderedAdjusted tenderedAdjusted) {
            db.Entry(tenderedAdjusted).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string shipmentNo) {
            TenderedAdjusted tenderedAdjusted = db.TenderedAdjusted.Find(shipmentNo);
            db.TenderedAdjusted.Remove(tenderedAdjusted);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
