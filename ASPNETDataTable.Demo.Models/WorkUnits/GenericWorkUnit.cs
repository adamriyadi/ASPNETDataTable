using ASPNETDataTable.Demo.Models;
using Ninject;
using System;

namespace ASPNETDataTable.Demo.WorkUnits
{
    public abstract class GenericWorkUnit : IGenericWorkUnit
    {
        protected bool disposed = false;
        protected internal DataTableContext context;

        public GenericWorkUnit(DataTableContext _context)
        {
            context = _context;
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    context.Dispose();

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}