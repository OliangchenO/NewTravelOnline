namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_Journal
    {
        public int id { get; set; }

        public Guid? uid { get; set; }

        [StringLength(50)]
        public string title { get; set; }

        [Column(TypeName = "ntext")]
        public string contents { get; set; }

        public DateTime? inputdate { get; set; }

        [StringLength(1)]
        public string flag { get; set; }

        [StringLength(100)]
        public string picurl { get; set; }

        public Guid? userid { get; set; }

        public string Destinationid { get; set; }

        public string Destination { get; set; }

        public int? views { get; set; }

        public int? comment { get; set; }

        [StringLength(255)]
        public string tags { get; set; }

        [StringLength(255)]
        public string coverpic { get; set; }

        public int? coverid { get; set; }

        public string albumid { get; set; }

        [StringLength(100)]
        public string seo { get; set; }

        public string DestinationList { get; set; }

        public int? FirstDestination { get; set; }

        public string parentid { get; set; }

        [Column(TypeName = "ntext")]
        public string seocontent { get; set; }

        [StringLength(1)]
        public string ImportFlag { get; set; }
        [StringLength(50)]
        public string Recom { get; set; }
        public DateTime? EditDate { get; set; }
        [StringLength(255)]
        public string coverpicurl { get; set; }
    }
}
