using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class TUserDb {
        private SCGLKPIDbContext db;
        public TUserDb() {
            db = new SCGLKPIDbContext();
        }

        public IEnumerable<TUser> GetAll() {
            return db.Tusers.ToList();
            }
        public TUser GetByID(int Id) {
            return db.Tusers.Find(Id);
            }
        public TUser GetByEmail(string Email)
        {
            return db.Tusers.Where(x => x.UserEmail == Email).FirstOrDefault();
        }
        public void Insert(TUser tuser) {
            db.Tusers.Add(tuser);
            Save();
            }
        public void Delete(int Id) {
            TUser tuser = db.Tusers.Find(Id);
            db.Tusers.Remove(tuser);
            Save();
            }
        public void Update(TUser tuser) {
            db.Entry(tuser).State = EntityState.Modified;
            Save();
            }

        public void Save() {
            db.SaveChanges();
            }
        }
    }
