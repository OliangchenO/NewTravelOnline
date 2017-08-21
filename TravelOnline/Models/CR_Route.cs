namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CR_Route
    {
        public int id { get; set; }

        public int? lineid { get; set; }

        public int? days { get; set; }

        [StringLength(100)]
        public string harbour { get; set; }

        [StringLength(50)]
        public string arrive { get; set; }

        [StringLength(50)]
        public string sail { get; set; }

        [StringLength(100)]
        public string visit { get; set; }
    }
}
