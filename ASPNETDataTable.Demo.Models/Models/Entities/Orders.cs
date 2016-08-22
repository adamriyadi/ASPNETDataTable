namespace ASPNETDataTable.Demo.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        public int id { get; set; }

        public int? customerid { get; set; }

        public int? productid { get; set; }

        [Column(TypeName = "numeric")]
        public decimal qty { get; set; }

        [Column(TypeName = "numeric")]
        public decimal totalprice { get; set; }

        public virtual Customers customer { get; set; }

        public virtual Products product { get; set; }
    }
}
