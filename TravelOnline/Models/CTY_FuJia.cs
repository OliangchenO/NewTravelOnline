namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CTY_FuJia
    {
        public int id { get; set; }

        public Guid? orderuid { get; set; }

        public int? autoid { get; set; }

        [StringLength(50)]
        public string bmb { get; set; }

        [StringLength(500)]
        public string bx { get; set; }

        [StringLength(500)]
        public string zp { get; set; }

        [StringLength(500)]
        public string e1 { get; set; }

        [StringLength(500)]
        public string e2 { get; set; }

        [StringLength(500)]
        public string e3 { get; set; }

        [StringLength(500)]
        public string e4 { get; set; }

        [StringLength(500)]
        public string e5 { get; set; }
    }
}
