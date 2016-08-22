namespace ASPNETDataTable.Demo.Models.Entities.Dto
{

    public class CustomerOrdersDto
    {
        public int customerid { get; set; }
        public int orderid { get; set; }
        public int productid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string productname { get; set; }
        public decimal qty { get; set; }
        public decimal totalprice { get; set; }
    }
}
