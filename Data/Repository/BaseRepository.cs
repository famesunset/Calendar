using System;
using Data.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Data.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        public string ConnectionString { get; }
        protected BaseRepository()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var config = builder.Build();
            ConnectionString = config["ConnectionStrings:DefaultConnection"];
        }
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
