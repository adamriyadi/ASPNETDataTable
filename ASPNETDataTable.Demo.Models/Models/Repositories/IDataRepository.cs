using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASPNETDataTable.Demo.Models.Repositories
{
    public interface IDataRepository<TEntity> : IDisposable where TEntity : class
    {
        DbSet<TEntity> GetCollection();

        IEnumerable<DbSet<TEntity>> GetAll();

        TEntity GetById(object id);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entity);
    }
}