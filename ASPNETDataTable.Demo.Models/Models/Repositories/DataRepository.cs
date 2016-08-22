using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ASPNETDataTable.Demo.Models.Repositories
{
    public class DataRepository<TEntity> : IDataRepository<TEntity> where TEntity : class
    {
        private bool disposed = false;

        private DataTableContext context;
        private DbSet<TEntity> Collection;

        public DataRepository(DataTableContext context)
        {
            this.context = context;
            Collection = context.Set<TEntity>();
        }

        public virtual DbSet<TEntity> GetCollection()
        {
            return Collection;
        }

        public virtual IEnumerable<DbSet<TEntity>> GetAll()
        {
            return (IEnumerable<DbSet<TEntity>>) Collection;
        }

        public virtual TEntity GetById(object id)
        {
            return Collection.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            Collection.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            Collection.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = Collection.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                Collection.Attach(entity);
            }
            Collection.Remove(entity);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    context.Dispose();

            disposed = true;
        }
    }
}