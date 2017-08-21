namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_Summary
    {
        public int id { get; set; }

        public Guid? uid { get; set; }

        public int? typeid { get; set; }

        [StringLength(50)]
        public string typename { get; set; }

        public int? desid { get; set; }

        public int? parentid { get; set; }

        [StringLength(50)]
        public string title { get; set; }

        [Column(TypeName = "ntext")]
        public string contents { get; set; }

        public Guid? userid { get; set; }

        public DateTime? inputdate { get; set; }

        public byte? flag { get; set; }
    }
}
