using System;

namespace ASPNet.DataTable.Models
{
    public class DTColumns
    {
        public string data { get; set; }
        public string name { get; set; }
        public Boolean searchable { get; set; }
        public Boolean orderable { get; set; }
        public DTSearch search { get; set; }
    }
}
