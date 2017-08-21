namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_SpecialLine_New
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? Stid { get; set; }

        public int? Lineid { get; set; }

        public int? SortNum { get; set; }

        public DateTime? EditTime { get; set; }

        public int? ErpID { get; set; }

        public short? LineType { get; set; }

        [StringLength(200)]
        public string Cname { get; set; }

        public int? Price { get; set; }

        public bool? Enable { get; set; }

        public short? TypeName { get; set; }

        [StringLength(100)]
        public string LineName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BeginDate { get; set; }

        [StringLength(200)]
        public string PhotoPath { get; set; }

        public short? Days { get; set; }

        public short? Types { get; set; }
        public int? TravelType { get; set; }
        public string DestName { get; set; }
        public string Destination { get; set; }
    }
}