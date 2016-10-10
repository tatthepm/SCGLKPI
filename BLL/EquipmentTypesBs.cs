using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;


namespace BLL {
   public class EquipmentTypesBs {
        private EquipmentTypesDb objDb;
        public EquipmentTypesBs() {
            objDb = new EquipmentTypesDb();
        }
        //GetAll
        public IEnumerable<EquipmentTypes> GetAll() {
            return objDb.GetAll();
        }

        //GetById
        public EquipmentTypes GetByID(int Id) {
            return objDb.GetByID(Id);
        }

        //GetByCode
        public EquipmentTypes GetByCode(string Code)
        {
            return objDb.GetByCode(Code);
        }

        //GetById
        public IEnumerable<EquipmentTypes> GetActive()
        {
            return objDb.GetActive();
        }

        //Insert
        public void Insert(EquipmentTypes EquipmentTypes) {
            objDb.Insert(EquipmentTypes);
        }

        //Update
        public void Update(EquipmentTypes EquipmentTypes) {
            objDb.Update(EquipmentTypes);
        }

        //Delete
        public void Delete(int Id) {
            objDb.Delete(Id);
        }
    }
}
