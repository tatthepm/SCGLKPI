using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;


namespace BLL {
    public class MenuTableBs {
        private MenuTableDb objDb;
        public MenuTableBs() {
            objDb = new MenuTableDb();
        }

        //GetAll
        public IQueryable<MenuTable> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public MenuTable GetByID(string departmentId, string sectionId, string kpiId) {
            return objDb.GetByID(departmentId, sectionId, kpiId);
        }

        //Insert
        public void Insert(MenuTable menuTable) {
            objDb.Insert(menuTable);
        }

        //Update
        public void Update(MenuTable menuTable) {
            objDb.Update(menuTable);
        }

        //Delete
        public void Delete(string departmentId, string sectionId, string kpiId) {
            objDb.Delete(departmentId, sectionId, kpiId);
        }
    }
}
