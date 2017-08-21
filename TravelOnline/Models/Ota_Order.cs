namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ota_Order
    {
        public int id { get; set; }

        public int? lineid { get; set; }

        public int? planid { get; set; }

        [StringLength(100)]
        public string linename { get; set; }

        public DateTime? begindate { get; set; }

        public int? ordernums { get; set; }

        public int? adults { get; set; }

        public int? childs { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? price { get; set; }

        [StringLength(50)]
        public string ordername { get; set; }

        [StringLength(50)]
        public string orderemail { get; set; }

        [StringLength(50)]
        public string ordermobile { get; set; }

        [StringLength(50)]
        public string ordertel { get; set; }

        [StringLength(500)]
        public string ordermemo { get; set; }

        [StringLength(20)]
        public string orderota { get; set; }

        public DateTime? ordertime { get; set; }

        public byte? orderflag { get; set; }
    }
}
