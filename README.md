# ASPNETDataTable
jqery DataTable (http://www.datatables.net) server side library for ASP.NET
 
### Dependency :
	System.Linq.Dynamic (>= 1.0.6)
	jquery.dataTables (>= 1.10)

### Sample :
``` cs
[Route("orders"), HttpPost]
public DTResponse<CustomerOrdersDto> GetOrder([FromBody] DTRequest dtRequest)
{
    using (var context = new DataTableContext())
    {
	IQueryable<CustomerOrdersDto> queryable = from c in context.customers
			      join o in context.orders on c.id equals o.customerid into cust_orders
			      from o in cust_orders
			      join p in context.products on o.productid equals p.id into cust_orders_product
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

	var dataTable = new ASPNetDataTable<CustomerOrdersDto>(dtRequest, queryable);
	dataTable.BuildDataTable();

	return dataTable.GetResponse();
    }
}
```


### With custom search & sort method :
``` cs
[Route("orders"), HttpPost]
public DTResponse<CustomerOrdersDto> GetOrder([FromBody] DTRequest dtRequest)
{
    using (var context = new DataTableContext())
    {
	IQueryable<CustomerOrdersDto>
	    query = from c in context.customers
			      join o in context.orders on c.id equals o.customerid into cust_orders
			      from o in cust_orders
			      join p in context.products on o.productid equals p.id into cust_orders_product
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

	var dataTable = new ASPNetDataTable<CustomerOrdersDto>(dtRequest, query);
	dataTable.BuildDataTable(MySearch, MySort);

	return dataTable.GetResponse();
    }
}

private IQueryable<CustomerOrdersDto> MySearch(IQueryable<CustomerOrdersDto> _query, IList<DTColumns> columns, DTSearch search)
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

private IQueryable<CustomerOrdersDto> MySort(IQueryable<CustomerOrdersDto> _query, IList<DTColumns> columns, IList<DTOrder> order)
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
```
