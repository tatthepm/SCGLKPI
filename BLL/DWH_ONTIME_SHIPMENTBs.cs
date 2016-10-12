using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using BOL;
using DAL;

namespace BLL {
    public class DWH_ONTIME_SHIPMENTBs {
        private DWH_ONTIME_SHIPMENTDb objDb;
        public DWH_ONTIME_SHIPMENTBs() {
            objDb = new DWH_ONTIME_SHIPMENTDb();
        }

        //GetAll
        public IEnumerable<DWH_ONTIME_SHIPMENT> GetAll() {
            return objDb.GetAll();
        }
        public IEnumerable<DWH_ONTIME_SHIPMENT> GetByFilter(List<Tuple<string,string>> expression,List<string> logic)
        {
            if (expression != null)
            {
                var param = Expression.Parameter(typeof(DWH_ONTIME_SHIPMENT), "p"); //it is the text, in this case, (p => p.{expression}) <-- p = DWH_ONTIME_DN
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
                    if (!String.IsNullOrEmpty(logic[i]))
                    {
                        switch (logic[i])
                        {
                            case "and":
                                binExp = Expression.AndAlso(binExp, binExp2);
                                break;
                            case "or":
                                binExp = Expression.OrElse(binExp, binExp2);
                                break;
                            default:
                                binExp = Expression.AndAlso(binExp, binExp2);
                                break;
                        }
                    }
                }
                var exp = Expression.Lambda<Func<DWH_ONTIME_SHIPMENT, bool>>(binExp, param);
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
        public DWH_ONTIME_SHIPMENT GetByID(string SHPMNTNO) {
            return objDb.GetByID(SHPMNTNO);
        }

        //Insert
        public void Insert(DWH_ONTIME_SHIPMENT DWH_ONTIME_SHIPMENT) {
            objDb.Insert(DWH_ONTIME_SHIPMENT);
        }

        //Update
        public void Update(DWH_ONTIME_SHIPMENT DWH_ONTIME_SHIPMENT) {
            objDb.Update(DWH_ONTIME_SHIPMENT);
        }

        //Delete
        public void Delete(string SHPMNTNO) {
            objDb.Delete(SHPMNTNO);
        }
    }
}
