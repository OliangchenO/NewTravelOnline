namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [Table("LineDest")]
    public partial class LineDest
    {
        [Key, Column(Order = 1)]
        public int? lineid { get; set; }
        [Key, Column(Order = 2)]
        public int? destid { get; set; }

        [StringLength(50)]
        public string days { get; set; }
    }
}