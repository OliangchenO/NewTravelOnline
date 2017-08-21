namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("fanli")]
    public partial class fanli
    {
        public int id { get; set; }

        public int? allotid { get; set; }

        public int? typeid { get; set; }

        [StringLength(50)]
        public string typename { get; set; }

        public int? rebate { get; set; }

        public int? thirdrebate { get; set; }

        public int? childrebate { get; set; }
    }
}
