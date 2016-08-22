using ASPNet.DataTable.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace ASPNet.DataTable
{
    public class ASPNetDataTable<TDto> where TDto : class
    {
        private IQueryable<TDto> query;
        private DTRequest dtRequest;
        private DTResponse<TDto> dtResponse;
        
        public ASPNetDataTable(DTRequest _dt_request, IQueryable<TDto> _query)
        {
            this.dtRequest = _dt_request;
            this.query = _query;
        }

        public DTResponse<TDto> GetResponse()
        {
            return dtResponse;
        }

        public IEnumerable<TDto> GetData()
        {
            return dtResponse.data;
        }

        public void BuildDataTable()
        {
            /// Count Total Record
            var recordsTotal = query.Count();

            /// Generate Filter
            #region Generate Where

            var filterText = dtRequest.search.value;
            if (!string.IsNullOrEmpty(filterText))
            {
                var where = "";
                var param = new List<string>();
                var i = 0;
                foreach (var item in dtRequest.columns)
                {
                    if (!string.IsNullOrEmpty(item.name))
                        where += item.name + ".ToString().ToLower().Contains(@" + i + ") OR ";
                    else
                        where += item.data + ".ToString().ToLower().Contains(@" + i + ") OR ";

                    if (!string.IsNullOrEmpty(item.search.value))
                        param.Add(item.search.value);
                    else
                        param.Add(filterText);

                    i++;
                }

                if (!string.IsNullOrEmpty(where))
                    where = where.Substring(0, where.Length - 4);

                query = query.Where(where, param.ToArray());
            }
            #endregion

            /// Generate OrderBy
            #region Generate Orderby
            var order = "";
            foreach (var item in dtRequest.order)
            {
                if (!string.IsNullOrEmpty(dtRequest.columns[item.column].name))
                    order = dtRequest.columns[item.column].name + " " + item.dir + ", ";
                else
                    order = dtRequest.columns[item.column].data + " " + item.dir + ", ";
            }

            if (!string.IsNullOrEmpty(order))
                order = order.Substring(0, order.Length - 2);

            query = query.OrderBy(order);
            #endregion

            /// Count Total Filtered
            var recordsFiltered = query.Count();

            /// Pagination
            query = query.Skip(dtRequest.start).Take(dtRequest.length);

            /// Create Response
            var data = query.ToList();
            dtResponse = new DTResponse<TDto>()
            {
                draw = dtRequest.draw,
                data = data,
                recordsFiltered = recordsFiltered,
                recordsTotal = recordsTotal
            };
        }
    }
}
