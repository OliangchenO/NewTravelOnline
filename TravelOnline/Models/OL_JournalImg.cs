namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_JournalImg
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        public Guid? uid { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid journalid { get; set; }

        public Guid? userid { get; set; }

        [StringLength(255)]
        public string originalname { get; set; }

        [StringLength(255)]
        public string uploadname { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime inputdate { get; set; }
    }
}
