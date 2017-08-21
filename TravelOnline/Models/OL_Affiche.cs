namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_Affiche
    {
        public int id { get; set; }

        [StringLength(100)]
        public string AfficheName { get; set; }

        [StringLength(10)]
        public string AfficheType { get; set; }

        [Column(TypeName = "ntext")]
        public string AfficheContent { get; set; }

        public Guid? EditUser { get; set; }

        public DateTime? EditTime { get; set; }

        public byte? AfficheFlag { get; set; }
    }
}
