namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_Prefer
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string UserEmail { get; set; }

        [StringLength(30)]
        public string UserName { get; set; }

        [StringLength(11)]
        public string Mobile { get; set; }

        public byte? Sex { get; set; }

        [StringLength(100)]
        public string birtyday { get; set; }

        public int? age { get; set; }

        public DateTime? LastLoginTime { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? price { get; set; }
    }
}
