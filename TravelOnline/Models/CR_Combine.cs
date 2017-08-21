namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CR_Combine
    {
        public int id { get; set; }

        public int? combineid { get; set; }

        public int? autoid { get; set; }

        public Guid? uid { get; set; }

        public DateTime? dotime { get; set; }
    }
}
