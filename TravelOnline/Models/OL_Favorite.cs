namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_Favorite
    {
        public int id { get; set; }

        public Guid? uid { get; set; }

        public int? lineid { get; set; }

        [StringLength(100)]
        public string linename { get; set; }

        public int? price { get; set; }

        public DateTime? inputdate { get; set; }
    }
}
