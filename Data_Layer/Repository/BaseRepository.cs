using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        public void IGet()
        {
            throw new NotImplementedException();
        }

        public void IInsert()
        {
            throw new NotImplementedException();
        }

        public void IUpdate()
        {
            throw new NotImplementedException();
        }
    }
}
