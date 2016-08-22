using System.Web.Http.ModelBinding;

namespace ASPNETDataTable.Demo.Controllers.Api
{
    public interface IBaseController
    {
        ModelStateDictionary ModelState { get; }
    }
}
