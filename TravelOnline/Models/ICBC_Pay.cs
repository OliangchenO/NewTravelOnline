namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ICBC_Pay
    {
        public int id { get; set; }

        public Guid? orderid { get; set; }

        [StringLength(20)]
        public string payid { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? amount { get; set; }

        public DateTime? date { get; set; }

        [StringLength(1)]
        public string flag { get; set; }
    }
}
