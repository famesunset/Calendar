using System;
using DataCore.Repository.Interfaces;

namespace DataCore.Repository
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
