using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL
{
    public class HubMastersBs
    {
        private HubMastersDb objDb;
        public HubMastersBs()
        {
            objDb = new HubMastersDb();
        }
        //GetAll
        public IQueryable<HubMasters> GetAll()
        {
            return objDb.GetAll();
        }

        //GetById
        public HubMasters GetByID(int Id)
        {
            return objDb.GetByID(Id);
        }

        //GetByCode
        public HubMasters GetByCode(string Code)
        {
            return objDb.GetByCode(Code);
        }

        //GetById
        public IQueryable<HubMasters> GetActive()
        {
            return objDb.GetActive();
        }

        //Insert
        public void Insert(HubMasters HubMasters)
        {
            objDb.Insert(HubMasters);
        }

        //Update
        public void Update(HubMasters HubMasters)
        {
            objDb.Update(HubMasters);
        }

        //Delete
        public void Delete(int Id)
        {
            objDb.Delete(Id);
        }
    }
}
