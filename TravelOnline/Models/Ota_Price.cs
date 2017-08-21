namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ota_Price
    {
        public int id { get; set; }

        public int? orderid { get; set; }

        public int? priceid { get; set; }

        [StringLength(50)]
        public string priceflag { get; set; }

        [StringLength(50)]
        public string pricetype { get; set; }

        [StringLength(100)]
        public string pricename { get; set; }

        public int? nums { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? price { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? allprice { get; set; }
    }
}
