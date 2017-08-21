namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_OL_View
    {
        public int id { get; set; }

        public int? desid { get; set; }

        [StringLength(50)]
        public string viewname { get; set; }

        [StringLength(50)]
        public string map_x { get; set; }

        [StringLength(50)]
        public string map_y { get; set; }

        public decimal? mapx { get; set; }

        public decimal? mapy { get; set; }
    }
}
