using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class DocReturnPendingDb {
        private SCGLKPIDbContext db;
        public DocReturnPendingDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<DocReturnPending> GetAll() {
            return db.DocReturnPendings.ToList();
        }

        //GetById
        public DocReturnPending GetByID(string deliveryNote) {
            return db.DocReturnPendings.Find(deliveryNote);
        }

        //Insert
        public void Insert(DocReturnPending outboundPending) {
            db.DocReturnPendings.Add(outboundPending);
            Save();
        }

        //Update
        public void Update(DocReturnPending docReturnPending) {
            db.Entry(docReturnPending).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string deliveryNote) {
            DocReturnPending docReturnPending = db.DocReturnPendings.Find(deliveryNote);
            db.DocReturnPendings.Remove(docReturnPending);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
