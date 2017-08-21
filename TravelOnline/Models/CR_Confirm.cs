namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CR_Confirm
    {
        public int id { get; set; }

        public int? lineid { get; set; }

        public string visit { get; set; }

        public string pay { get; set; }

        public string cancel { get; set; }

        public string visa { get; set; }

        public string change { get; set; }

        public string other { get; set; }

        public string views { get; set; }
    }
}
