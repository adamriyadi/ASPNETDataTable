using ASPNETDataTable.Demo.Controllers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETDataTable.Demo.Services
{
    public interface IService
    {
        void Register(IBaseController controller);
    }
}