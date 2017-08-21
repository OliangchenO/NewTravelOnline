namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_View
    {
        public int id { get; set; }

        public Guid? uid { get; set; }

        public int? desid { get; set; }

        [StringLength(50)]
        public string viewname { get; set; }

        [StringLength(250)]
        public string picurl { get; set; }

        [StringLength(250)]
        public string tags { get; set; }

        [StringLength(100)]
        public string tel { get; set; }

        [StringLength(100)]
        public string address { get; set; }

        [StringLength(250)]
        public string ticket { get; set; }

        [StringLength(250)]
        public string ticketmemo { get; set; }

        [StringLength(250)]
        public string opentime { get; set; }

        [StringLength(50)]
        public string map_x { get; set; }

        [StringLength(50)]
        public string map_y { get; set; }

        [StringLength(10)]
        public string map_size { get; set; }

        [StringLength(250)]
        public string visitseason { get; set; }

        [StringLength(250)]
        public string visittime { get; set; }

        public string intro { get; set; }

        public string viewpoint { get; set; }

        public string traffic { get; set; }

        public string memo { get; set; }

        [StringLength(1)]
        public string hotflag { get; set; }

        [StringLength(200)]
        public string PinYin { get; set; }

        [StringLength(50)]
        public string SortPinYin { get; set; }

        [StringLength(100)]
        public string SeoName { get; set; }
    }
}
