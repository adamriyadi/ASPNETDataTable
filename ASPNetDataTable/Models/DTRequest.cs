using System.Collections.Generic;

namespace ASPNet.DataTable.Models
{
    public class DTRequest
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public DTSearch search { get; set; }
        public List<DTOrder> order { get; set; }
        public List<DTColumns> columns { get; set; }
    }
}
