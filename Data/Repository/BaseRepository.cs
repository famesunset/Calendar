using System;
using Data.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Data.Repository
{
    public abstract class BaseRepository
    {
        public string ConnectionString { get; }
        protected BaseRepository()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var config = builder.Build();
            ConnectionString = config["ConnectionStrings:DefaultConnection"];
        }
    }
}
