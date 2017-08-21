namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CR_Visit
    {
        public int id { get; set; }

        public int? lineid { get; set; }

        public int? days { get; set; }

        [StringLength(100)]
        public string vtitle { get; set; }

        [StringLength(100)]
        public string visitname { get; set; }

        [StringLength(50)]
        public string stay { get; set; }

        [StringLength(50)]
        public string sight { get; set; }

        [StringLength(50)]
        public string dinner { get; set; }

        [Column(TypeName = "ntext")]
        public string intro { get; set; }

        public int? price { get; set; }

        public int? nums { get; set; }

        [StringLength(50)]
        public string vdate { get; set; }

        [StringLength(100)]
        public string vmemo { get; set; }

        [StringLength(1)]
        public string sellflag { get; set; }
    }
}
