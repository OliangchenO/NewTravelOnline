namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_FlashAD
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string AdName { get; set; }

        [StringLength(100)]
        public string AdPicUrl { get; set; }

        [StringLength(100)]
        public string AdSecPicUrl { get; set; }

        [StringLength(100)]
        public string AdPageUrl { get; set; }

        [StringLength(20)]
        public string AdFlag { get; set; }

        public byte? AdSort { get; set; }

        public DateTime? EditTime { get; set; }

        public int? MisClassId { get; set; }

        [StringLength(1)]
        public string HideFlag { get; set; }

        [StringLength(10)]
        public string BackGround { get; set; }
    }
}
