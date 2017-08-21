namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InitData")]
    public partial class InitData
    {
        public int id { get; set; }

        [StringLength(50)]
        public string ftype { get; set; }

        [StringLength(50)]
        public string fname { get; set; }

        [StringLength(100)]
        public string dataname { get; set; }

        public int? sort { get; set; }
    }
}
