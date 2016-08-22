using ASPNet.DataTable.Models;
using System;
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

        public void BuildDataTable()
        {
            BuildDataTable(Search, Sort);
        }

        public void BuildDataTable(Func<IQueryable<TDto>, IList<DTColumns>, DTSearch, IQueryable<TDto>> _search)
        {
            BuildDataTable(_search, Sort);
        }

        public void BuildDataTable(Func<IQueryable<TDto>, IList<DTColumns>, IList<DTOrder>, IQueryable<TDto>> _sort)
        {
            BuildDataTable(Search, _sort);
        }

        public void BuildDataTable(
            Func<IQueryable<TDto>, IList<DTColumns>, DTSearch, IQueryable<TDto>> _search,
            Func<IQueryable<TDto>, IList<DTColumns>, IList<DTOrder>, IQueryable<TDto>> _sort)
        {
            /// Count Total Record
            var recordsTotal = query.Count();

            /// Generate Filter
            var filterText = dtRequest.search.value;
            if (!string.IsNullOrEmpty(filterText))
                query = _search(query, dtRequest.columns, dtRequest.search);

            /// Generate OrderBy
            query = _sort(query, dtRequest.columns, dtRequest.order);

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

        public DTResponse<TDto> GetResponse()
        {
            return dtResponse;
        }

        public IEnumerable<TDto> GetData()
        {
            return dtResponse.data;
        }

        private IQueryable<TDto> Search(IQueryable<TDto> _query, IList<DTColumns> columns, DTSearch search)
        {
            var filterText = search.value;
            var where = "";
            var param = new List<string>();
            var i = 0;
            foreach (var item in columns)
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

            return _query.Where(where, param.ToArray());
        }

        private IQueryable<TDto> Sort(IQueryable<TDto> _query, IList<DTColumns> columns, IList<DTOrder> order)
        {
            var sort = "";
            foreach (var item in order)
            {
                if (!string.IsNullOrEmpty(columns[item.column].name))
                    sort = columns[item.column].name + " " + item.dir + ", ";
                else
                    sort = columns[item.column].data + " " + item.dir + ", ";
            }

            if (!string.IsNullOrEmpty(sort))
                sort = sort.Substring(0, sort.Length - 2);

            return _query.OrderBy(sort);
        }
    }
}
