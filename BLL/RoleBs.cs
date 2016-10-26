using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL {
   public class RoleBs {
       private RoleDb objDb;
       public RoleBs() {
           objDb = new RoleDb();
           }

       //GetAll
       public IQueryable<Role> GetAll() {
           return objDb.GetAll();
           }

       //GetById
       public Role GetByID(string roleId) {
           return objDb.GetByID(roleId);
           }

       //Insert
       public void Insert(Role role) {
           objDb.Insert(role);
           }

       //Update
       public void Update(Role role) {
           objDb.Update(role);
           }

       //Delete
       public void Delete(string roleId) {
           objDb.Delete(roleId);
           }
        }
    }
