using System.Collections.Generic;

namespace ASPNet.DataTable.Models
{
    public class DTResponse<TDto> where TDto : class
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public IList<TDto> data { get; set; }
        public string error { get; set; }
    }
}
