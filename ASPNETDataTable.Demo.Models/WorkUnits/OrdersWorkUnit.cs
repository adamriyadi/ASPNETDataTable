using ASPNETDataTable.Demo.Models;
using ASPNETDataTable.Demo.Models.Entities;
using ASPNETDataTable.Demo.Models.Entities.Dto;
using ASPNETDataTable.Demo.Models.Repositories;
using System.Linq;

namespace ASPNETDataTable.Demo.WorkUnits
{
    public class OrdersWorkUnit : GenericWorkUnit
    {
        private IDataRepository<Customers> _customers_repository;
        private IDataRepository<Orders> _orders_repository;
        private IDataRepository<Products> _products_repository;

        public OrdersWorkUnit(DataTableContext _context) : base(_context)
        {
        }

        public IDataRepository<Customers> CustomersRepository
        {
            get
            {
                if (_customers_repository == null) _customers_repository = new DataRepository<Customers>(context);
                return _customers_repository;
            }
        }

        public IDataRepository<Orders> OrdersRepository
        {
            get
            {
                if (_orders_repository == null) _orders_repository = new DataRepository<Orders>(context);
                return _orders_repository;
            }
        }

        public IDataRepository<Products> ProductsRepository
        {
            get
            {
                if (_products_repository == null) _products_repository = new DataRepository<Products>(context);
                return _products_repository;
            }
        }

        public IQueryable<CustomerOrdersDto> GetCustomerOrders()
        {
            var customer_orders = from c in CustomersRepository.GetCollection()
                                  join o in OrdersRepository.GetCollection() on c.id equals o.customerid into cust_orders
                                  from o in cust_orders
                                  join p in ProductsRepository.GetCollection() on o.productid equals p.id into cust_orders_product
                                  from p in cust_orders_product
                                  select new CustomerOrdersDto()
                                  {
                                      customerid = c.id,
                                      orderid = o.id,
                                      productid = p.id,
                                      firstname = c.firstname,
                                      lastname = c.lastname,
                                      productname = p.productname,
                                      qty = o.qty,
                                      totalprice = o.totalprice
                                  };

            return customer_orders;
        }

        public CustomerOrdersDto GetCustomerOrder(int id)
        {
            var customer_order = GetCustomerOrders().Where(x => x.orderid == id).FirstOrDefault();

            return customer_order;
        }
    }
}