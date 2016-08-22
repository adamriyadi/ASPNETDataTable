using ASPNETDataTable.Demo.Models;
using ASPNETDataTable.Demo.Models.Entities.Dto;
using ASPNETDataTable.Demo.WorkUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETDataTable.Demo.Services
{
    public class OrdersService : BaseService, IDisposable
    {
        private bool disposed = false;

        private OrdersWorkUnit workUnit;
        public OrdersService(DataTableContext _context)
        {
            workUnit = new OrdersWorkUnit(_context);
        }

        public IQueryable<CustomerOrdersDto> GetCustomerOrders()
        {
            return workUnit.GetCustomerOrders();
        }

        public CustomerOrdersDto GetCustomerOrder(int id)
        {
            return workUnit.GetCustomerOrder(id);
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
                    workUnit.Dispose();

            disposed = true;
        }
    }
}