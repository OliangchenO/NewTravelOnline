namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_Plan
    {
        public int id { get; set; }

        public int? lineid { get; set; }

        public int? planid { get; set; }

        public DateTime? begindate { get; set; }

        public DateTime? enddate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? price { get; set; }

        public int? seats { get; set; }
    }
}
