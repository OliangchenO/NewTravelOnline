namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_FriendLink
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string LinkName { get; set; }

        [StringLength(150)]
        public string LinkUrl { get; set; }

        [StringLength(100)]
        public string LinkPic { get; set; }

        public byte? LinkType { get; set; }

        public int? rankid { get; set; }
    }
}
