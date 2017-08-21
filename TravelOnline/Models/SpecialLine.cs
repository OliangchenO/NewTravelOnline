namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpecialLine")]
    public partial class SpecialLine
    {
        public int Id { get; set; }

        public int? Stid { get; set; }

        public int? Lineid { get; set; }

        public int? SortNum { get; set; }

        public DateTime? EditTime { get; set; }

        public int? MisLineId { get; set; }

        [StringLength(100)]
        public string LineName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        [StringLength(100)]
        public string AreaId { get; set; }

        public int? LineClass { get; set; }

        public DateTime? PlanDate { get; set; }
        public string PlanDateStr { get; set; }

        [StringLength(100)]
        public string Destination { get; set; }

        [StringLength(100)]
        public string Pics { get; set; }

        [StringLength(10)]
        public string LineType { get; set; }

        [StringLength(1)]
        public string PlanType { get; set; }

        [StringLength(250)]
        public string LineFeature { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string PlanTypeName { get; set; }

        public byte? LineDays { get; set; }

        [StringLength(10)]
        public string Standard { get; set; }

        public int? Topic { get; set; }
    }
}
