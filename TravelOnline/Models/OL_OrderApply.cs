namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_OrderApply
    {
        public int id { get; set; }

        public Guid? orderid { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        public Guid? OrderUser { get; set; }

        public DateTime? inputdate { get; set; }

        [StringLength(50)]
        public string applyflag { get; set; }

        public int? originalid { get; set; }

        public int? updateid { get; set; }

        [StringLength(1)]
        public string flag { get; set; }

        [StringLength(50)]
        public string douser { get; set; }

        public DateTime? dotime { get; set; }

        public string infos { get; set; }

        [StringLength(50)]
        public string peoples { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? price { get; set; }

        public int? OrderNums { get; set; }

        public string ChangInfo { get; set; }
    }
}
