namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CR_Ship
    {
        public int id { get; set; }

        public Guid? uid { get; set; }

        public int? comid { get; set; }

        public int? series { get; set; }

        [StringLength(50)]
        public string seriesname { get; set; }

        [StringLength(100)]
        public string cname { get; set; }

        [StringLength(100)]
        public string ename { get; set; }

        [StringLength(50)]
        public string tonnage { get; set; }

        [StringLength(50)]
        public string native { get; set; }

        [StringLength(50)]
        public string capacity { get; set; }

        [StringLength(50)]
        public string length { get; set; }

        [StringLength(50)]
        public string width { get; set; }

        [StringLength(50)]
        public string waterline { get; set; }

        public short? deck { get; set; }

        [StringLength(50)]
        public string speed { get; set; }

        [StringLength(50)]
        public string firstseaway { get; set; }

        [StringLength(50)]
        public string rooms { get; set; }

        [StringLength(50)]
        public string voltage { get; set; }

        [Column(TypeName = "ntext")]
        public string feature { get; set; }

        [Column(TypeName = "ntext")]
        public string restaurant { get; set; }

        [Column(TypeName = "ntext")]
        public string collection { get; set; }

        [Column(TypeName = "ntext")]
        public string meeting { get; set; }

        [Column(TypeName = "ntext")]
        public string bar { get; set; }

        [Column(TypeName = "ntext")]
        public string amusement { get; set; }

        [Column(TypeName = "ntext")]
        public string others { get; set; }

        [Column(TypeName = "ntext")]
        public string free { get; set; }

        [Column(TypeName = "ntext")]
        public string charge { get; set; }

        public Guid? userid { get; set; }

        [StringLength(30)]
        public string username { get; set; }
    }
}
