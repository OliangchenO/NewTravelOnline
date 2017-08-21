namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ota_Pay
    {
        public int id { get; set; }

        public int? orderid { get; set; }

        [StringLength(50)]
        public string tradeno { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? payprice { get; set; }

        public DateTime? paytime { get; set; }

        [StringLength(500)]
        public string paycontent { get; set; }

        [StringLength(50)]
        public string paytype { get; set; }
    }
}
