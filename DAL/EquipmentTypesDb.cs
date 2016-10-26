using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL {
    public class EquipmentTypesDb {
        private SCGLKPIDbContext db;
        public EquipmentTypesDb() {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<EquipmentTypes> GetAll() {
            return db.EquipmentTypes;
        }

        //GetById
        public EquipmentTypes GetByID(int Id) {
            return db.EquipmentTypes.Find(Id);
        }

        //GetByCode
        public EquipmentTypes GetByCode(string Code)
        {
            return db.EquipmentTypes.Where(x => x.Eqmt_Code == Code).FirstOrDefault();
        }

        //GetByCode
        public IQueryable<EquipmentTypes> GetActive()
        {
            return db.EquipmentTypes.Where(x => x.IsActive == true);
        }

        //Insert
        public void Insert(EquipmentTypes EquipmentTypes) {
            db.EquipmentTypes.Add(EquipmentTypes);
            Save();
        }

        //Update
        public void Update(EquipmentTypes EquipmentTypes) {
            db.Entry(EquipmentTypes).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id) {
            EquipmentTypes EquipmentTypes = db.EquipmentTypes.Find(Id);
            EquipmentTypes.IsDeleted = true;
            db.Entry(EquipmentTypes).State = EntityState.Modified;
            Save();
        }

        //Save
        public void Save() {
            db.SaveChanges();
        }
    }
}
