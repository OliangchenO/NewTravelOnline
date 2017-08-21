namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_MemberOrder
    {
        [Key]
        [Column(Order = 0)]
        public Guid OrderId { get; set; }

        [StringLength(10)]
        public string ProductType { get; set; }

        public int? ProductClass { get; set; }

        public int? LineID { get; set; }

        [StringLength(100)]
        public string LineName { get; set; }

        public DateTime? BeginDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        public Guid? OrderUser { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AutoId { get; set; }

        public byte? LineDays { get; set; }

        public int? DeptId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public DateTime? enddate { get; set; }

        [StringLength(10)]
        public string Integral { get; set; }
    }
}
