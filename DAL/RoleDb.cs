using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;


namespace DAL {
    public class RoleDb {
        private SCGLKPIDbContext db;
        public RoleDb() {
            db = new SCGLKPIDbContext();
            }
        //GetAll
        public IEnumerable<Role> GetAll() {
            return db.Roles.ToList();
            }

        //GetById
        public Role GetByID(string roleId) {
            return db.Roles.Find(roleId);
            }

        //Insert
        public void Insert(Role role) {
            db.Roles.Add(role);
            Save();
            }

        //Update
        public void Update(Role role) {
            db.Entry(role).State = EntityState.Modified;
            Save();
            }

        //Delete
        public void Delete(string roleId) {
            Role role = db.Roles.Find(roleId);
            db.Roles.Remove(role);
            Save();
            }

        //Save
        public void Save() {
            db.SaveChanges();
            }
        }
    }
