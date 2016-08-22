using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASPNETDataTable.Demo.Controllers.Api;

namespace ASPNETDataTable.Demo.Services
{
    public abstract class BaseService : IService
    {
        private bool disposed = false;

        private IBaseController _controller;
        protected IBaseController Controller { get { return _controller; } }

        public void Register(IBaseController controller)
        {
            _controller = controller;
        }
    }
}