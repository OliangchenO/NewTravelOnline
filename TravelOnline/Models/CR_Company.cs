namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CR_Company
    {
        public int id { get; set; }

        public Guid? uid { get; set; }

        [StringLength(100)]
        public string cname { get; set; }

        [StringLength(100)]
        public string ename { get; set; }

        [Column(TypeName = "ntext")]
        public string intro { get; set; }

        [StringLength(50)]
        public string picurl { get; set; }

        public Guid? userid { get; set; }

        [StringLength(30)]
        public string username { get; set; }
    }
}
