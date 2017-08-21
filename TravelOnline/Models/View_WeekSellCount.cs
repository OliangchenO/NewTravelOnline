namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_WeekSellCount
    {
        public Guid Id { get; set; }

        [StringLength(100)]
        public string LineName { get; set; }

        public int? MisLineId { get; set; }

        public int? LineClass { get; set; }

        public byte? Preferences { get; set; }

        public byte? Recommend { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        [StringLength(10)]
        public string LineType { get; set; }

        public byte? LineDays { get; set; }

        public int? PV { get; set; }

        public byte? Ranking { get; set; }

        public byte? LineSort { get; set; }

        [StringLength(100)]
        public string Pics { get; set; }

        public byte? Sale { get; set; }

        public int? sellcount { get; set; }

        public DateTime? PlanDate { get; set; }

        public string Destinationid { get; set; }

        public int? FirstDestination { get; set; }
    }
}
