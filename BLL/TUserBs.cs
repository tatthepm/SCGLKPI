using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
    public class TUserBs {
        private TUserDb objDb;
        public TUserBs() {
            objDb = new TUserDb();
            }

        //GetAll
        public IEnumerable<TUser> GetAll() {
            return objDb.GetAll();
            }

        //GetById
        public TUser GetByID(int Id) {
            return objDb.GetByID(Id);
            }

        //GetByEmail
        public TUser GetByEmail(string Email)
        {
            return objDb.GetByEmail(Email);
        }

        //Insert
        public void Insert(TUser tuser) {
            objDb.Insert(tuser);
            }

        //Update
        public void Update(TUser tuser) {
            objDb.Update(tuser);
            }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
            }
        }
    }
