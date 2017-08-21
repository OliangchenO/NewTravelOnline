namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("XiRongOrder")]
    public partial class XiRongOrder
    {
        public int id { get; set; }

        public int? xruserid { get; set; }

        public int? xrorderid { get; set; }

        public int? autoid { get; set; }

        [StringLength(1)]
        public string flag { get; set; }

        public int? lineid { get; set; }

        public DateTime? orderdate { get; set; }
    }
}
