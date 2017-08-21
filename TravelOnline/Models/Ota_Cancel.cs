namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ota_Cancel
    {
        public int id { get; set; }

        public int? orderid { get; set; }

        public DateTime? dotime { get; set; }
    }
}
