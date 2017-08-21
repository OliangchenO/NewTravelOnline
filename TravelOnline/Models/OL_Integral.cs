namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_Integral
    {
        public int id { get; set; }

        public Guid? uid { get; set; }

        public Guid? orderid { get; set; }

        public int? lineid { get; set; }

        public int? integral { get; set; }

        public DateTime? getdate { get; set; }

        [StringLength(1)]
        public string flag { get; set; }

        public int? dept { get; set; }

        public DateTime? enddate { get; set; }
    }
}
