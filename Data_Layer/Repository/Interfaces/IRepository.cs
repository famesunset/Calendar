using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace Data_Layer.Repository
{
    public interface IRepository<T> where T : class
    {
        void IGet();
        void IUpdate();
        void IInsert();
    }
}
