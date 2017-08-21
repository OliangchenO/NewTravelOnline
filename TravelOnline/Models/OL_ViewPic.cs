namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_ViewPic
    {
        public int id { get; set; }

        public int? desid { get; set; }

        public int? viewid { get; set; }

        [StringLength(100)]
        public string picname { get; set; }

        public string picmemo { get; set; }

        [StringLength(250)]
        public string picurl { get; set; }
    }
}
