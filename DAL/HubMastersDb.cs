using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BOL;

namespace DAL
{
    public class HubMastersDb
    {
        private SCGLKPIDbContext db;
        public HubMastersDb()
        {
            db = new SCGLKPIDbContext();
        }
        //GetAll
        public IQueryable<HubMasters> GetAll()
        {
            return db.HubMasters;
        }

        //GetById
        public HubMasters GetByID(int Id)
        {
            return db.HubMasters.Find(Id);
        }

        //GetByCode
        public HubMasters GetByCode(string Code)
        {
            return db.HubMasters.Where(x => x.Hub_Id == Code).FirstOrDefault();
        }

        //GetByCode
        public IQueryable<HubMasters> GetActive()
        {
            return db.HubMasters.Where(x => x.IsActive == true);
        }

        //Insert
        public void Insert(HubMasters HubMasters)
        {
            db.HubMasters.Add(HubMasters);
            Save();
        }

        //Update
        public void Update(HubMasters HubMasters)
        {
            db.Entry(HubMasters).State = EntityState.Modified;
            Save();
        }

        //Delete
        public void Delete(int Id)
        {
            HubMasters HubMasters = db.HubMasters.Find(Id);
            HubMasters.IsDeleted = true;
            db.Entry(HubMasters).State = EntityState.Modified;
            Save();
        }

        //Save
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
