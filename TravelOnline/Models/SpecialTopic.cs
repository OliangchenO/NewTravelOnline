namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpecialTopic")]
    public partial class SpecialTopic
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Types { get; set; }

        [StringLength(50)]
        public string Cname { get; set; }

        public int? SortNum { get; set; }

        public DateTime? EditTime { get; set; }

        public string Url { get; set; }

        [StringLength(500)]
        public string LineType { get; set; }

        public string Destinationid { get; set; }

        public string Destination { get; set; }

        public string DestinationList { get; set; }
        
        public List<SpecialLine> SpecialLineList { get; set; }
    }
}
