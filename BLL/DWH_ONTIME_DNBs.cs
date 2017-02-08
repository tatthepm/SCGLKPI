using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;
using System.Linq.Expressions;

namespace BLL
{
    public class DWH_ONTIME_DNBs
    {
        private DWH_ONTIME_DNDb objDb;
        public DWH_ONTIME_DNBs()
        {
            objDb = new DWH_ONTIME_DNDb();
        }

        //GetAll
        public IQueryable<DWH_ONTIME_DN> GetAll()
        {
            return objDb.GetAll();
        }

        public IQueryable<DWH_ONTIME_DN> GetByDate(DateTime? FromDateSearch, DateTime? ToDateSearch)
        {
            return objDb.GetByDate(FromDateSearch,ToDateSearch);
        }
        public IQueryable<DWH_ONTIME_DN> GetByFilter(DateTime? FromDateSearch, DateTime? ToDateSearch,List<Tuple<string, string>> expression)
        {
            if (expression != null)
            {
                var param = Expression.Parameter(typeof(DWH_ONTIME_DN), "p"); //it is the text, in this case, (p => p.{expression}) <-- p = DWH_ONTIME_DN
                var binExp = Expression.Equal(
                        Expression.Property(param, expression[0].Item1),
                        Expression.Constant(expression[0].Item2)
                    );

                for (int i = 1; i < expression.Count; i++)
                {
                    var binExp2 = Expression.Equal(
                            Expression.Property(param, expression[i].Item1),
                            Expression.Constant(expression[i].Item2)
                        );
                    binExp = Expression.AndAlso(binExp, binExp2);
                }
                var exp = Expression.Lambda<Func<DWH_ONTIME_DN, bool>>(binExp,param);

                var binExp3 = helperClass.MyGreaterThan(
                           Expression.Property(param, "ACTGIDATE"),
                           Expression.Constant(FromDateSearch)
                       );
                binExp = Expression.AndAlso(binExp, binExp3);

                var binExp4 = helperClass.MyLessThan(
                            Expression.Property(param, "ACTGIDATE"),
                            Expression.Constant(ToDateSearch)
                        );
                binExp = Expression.AndAlso(binExp, binExp4);

                return objDb.GetByFilter(exp);
            }
            else
            { return null; }
        }
        
        //GetCount
        public int GetCount()
        {
            return objDb.GetCount();
        }

        //GetById
        public DWH_ONTIME_DN GetByID(string DELVNO)
        {
            return objDb.GetByID(DELVNO);
        }

        //Insert
        public void Insert(DWH_ONTIME_DN DWH_ONTIME_DN)
        {
            objDb.Insert(DWH_ONTIME_DN);
        }

        //Update
        public void Update(DWH_ONTIME_DN DWH_ONTIME_DN)
        {
            objDb.Update(DWH_ONTIME_DN);
        }

        //Delete
        public void Delete(string DELVNO)
        {
            objDb.Delete(DELVNO);
        }
    }
}
