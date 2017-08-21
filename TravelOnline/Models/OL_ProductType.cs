namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_ProductType
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string ProductType { get; set; }

        [StringLength(20)]
        public string ProductName { get; set; }

        public byte? ProductSort { get; set; }

        public int? MisClassId { get; set; }

        public string Destinationid { get; set; }

        public string Destination { get; set; }

        public string DestinationList { get; set; }

        public int? FirstDestination { get; set; }
    }
}
