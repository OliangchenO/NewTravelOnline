namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SeoLink")]
    public partial class SeoLink
    {
        public int id { get; set; }

        [StringLength(50)]
        public string keyword { get; set; }

        [StringLength(500)]
        public string url { get; set; }

        public int? serchnum { get; set; }

        public int? rank { get; set; }

        public int? keylength { get; set; }
    }
}
