﻿namespace Data_Layer.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void IGet();
        void IUpdate();
        void IInsert();
    }
}
