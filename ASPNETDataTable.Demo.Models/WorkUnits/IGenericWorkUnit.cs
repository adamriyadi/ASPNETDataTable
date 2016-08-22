using ASPNETDataTable.Demo.Models;
using System;

namespace ASPNETDataTable.Demo.WorkUnits
{
    public interface IGenericWorkUnit : IDisposable
    {
         int Save();
    }
}