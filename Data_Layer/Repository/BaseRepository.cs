using System;
using Data_Layer.Repository.Interfaces;

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
