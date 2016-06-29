using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class MenuTableDb {
        private SCGLKPIDbContext db;
        public MenuTableDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IEnumerable<MenuTable> GetAll() {
            return db.MenuTables.ToList();
        }

        //GetById
        public MenuTable GetByID(string departmentId, string sectionId, string kpiId) {
            return db.MenuTables.Find(departmentId, sectionId, kpiId);
        }

        //Insert
        public void Insert(MenuTable menuTable) {
            db.MenuTables.Add(menuTable);
            Save();
        }

        //Update
        public void Update(MenuTable menuTable) {
            db.Entry(menuTable).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(string departmentId, string sectionId, string kpiId) {
            MenuTable menuTable = db.MenuTables.Find(departmentId, sectionId, kpiId);
            db.MenuTables.Remove(menuTable);
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
