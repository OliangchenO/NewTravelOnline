namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_SpecialLineTemp_New
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Stid { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SortNum { get; set; }

        [StringLength(100)]
        public string LineName { get; set; }

        public int? Price { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BeginDate { get; set; }

        public short? LineType { get; set; }

        public int? Topic { get; set; }

        public int? ErpID { get; set; }

        [StringLength(200)]
        public string Cname { get; set; }

        public bool? Enable { get; set; }

        public short? TypeName { get; set; }

        [StringLength(200)]
        public string PhotoPath { get; set; }

        public short? Days { get; set; }

        public short? Types { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? lineId { get; set; }
        public int? TravelType { get; set; }
        public string DestName { get; set; }
        public string Destination { get; set; }
    }
}