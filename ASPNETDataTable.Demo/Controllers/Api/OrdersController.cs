using ASPNet.DataTable.Models;
using ASPNet.DataTable;
using ASPNETDataTable.Demo.Models.Entities.Dto;
using ASPNETDataTable.Demo.Services;
using ASPNETDataTable.Demo.WorkUnits;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;

namespace ASPNETDataTable.Demo.Controllers.Api
{
    [RoutePrefix("api")]
    public class OrdersController : BaseApiController
    {
        private bool disposed;
        private OrdersService service;

        public OrdersController(OrdersService _service)
        {
            service = _service;
            service.Register(this);
        }

        // GET: api/Orders
        [Route("orders"), HttpPost]
        public DTResponse<CustomerOrdersDto> Get([FromBody] DTRequest dtRequest)
        {

            var customer_orders = service.GetCustomerOrders();

            var dataTable = new ASPNetDataTable<CustomerOrdersDto>(dtRequest, customer_orders);
            dataTable.BuildDataTable();

            return dataTable.GetResponse();
        }

        // GET: api/Orders/5
        [Route("order/{id}")]
        public CustomerOrdersDto Get(int id)
        {
            var customer_order = service.GetCustomerOrder(id);

            return customer_order;
        }
    }
}
