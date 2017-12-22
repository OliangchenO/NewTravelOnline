namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_Line
    {
        public Guid Id { get; set; }

        public int? MisLineId { get; set; }

        [StringLength(10)]
        public string LineType { get; set; }

        [StringLength(100)]
        public string LineTypeName { get; set; }

        [StringLength(100)]
        public string LineName { get; set; }

        public int? LineClass { get; set; }

        public byte? LineDays { get; set; }

        [StringLength(10)]
        public string Standard { get; set; }

        public int? Topic { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        [StringLength(100)]
        public string AreaId { get; set; }

        [StringLength(250)]
        public string LineFeature { get; set; }

        public DateTime? PlanDate { get; set; }

        public byte? Preferences { get; set; }

        public byte? Recommend { get; set; }

        public byte? LineSort { get; set; }

        public byte? Ranking { get; set; }

        public int? PV { get; set; }

        public byte? Sale { get; set; }

        public DateTime? EditTime { get; set; }

        public int? DeptId { get; set; }

        [StringLength(100)]
        public string Pics { get; set; }

        [Column(TypeName = "ntext")]
        public string Pdates { get; set; }

        [StringLength(1)]
        public string SpFlag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? msprice { get; set; }

        public int? yfk { get; set; }

        [StringLength(100)]
        public string Tags { get; set; }

        public int? CuisesId { get; set; }

        public int? SfId { get; set; }

        public int? wwwyh { get; set; }

        [StringLength(100)]
        public string VisaId { get; set; }

        public int? Shipid { get; set; }

        [StringLength(50)]
        public string Planid { get; set; }

        [StringLength(100)]
        public string dinner { get; set; }

        public short? AgeLimit { get; set; }

        [StringLength(1)]
        public string VisitSell { get; set; }

        [StringLength(1)]
        public string StateFlag { get; set; }

        [StringLength(1)]
        public string CruisesReport { get; set; }

        public string Destinationid { get; set; }

        public string Destination { get; set; }

        public string DestinationName { get; set; }

        public string DestinationList { get; set; }

        public int? FirstDestination { get; set; }

        public byte? IndexRecom { get; set; }

        public byte? NewRecom { get; set; }

        public string NewSortTime { get; set; }

        [StringLength(100)]
        public string BigPics { get; set; }

        [StringLength(2)]
        public string famous { get; set; }

        public string viewids { get; set; }

        public string viewlist { get; set; }

        public string viewname { get; set; }

        [StringLength(10)]
        public string Integral { get; set; }

        public byte? WeChat { get; set; }

        public DateTime? WeChatSortTime { get; set; }

        public DateTime? TopBegin { get; set; }

        public DateTime? TopEnd { get; set; }

        [StringLength(1)]
        public string PlanType { get; set; }
    }
}
