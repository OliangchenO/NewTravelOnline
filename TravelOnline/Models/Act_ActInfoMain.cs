namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Act_ActInfoMain
    {
        [Key]
        public int ActInfoMain_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ActivityName { get; set; }

        public DateTime? ActivityStartTime { get; set; }

        public DateTime? ActivityEndTime { get; set; }

        [StringLength(20)]
        public string Start { get; set; }

        public int? MaxNum { get; set; }

        public int? MinNum { get; set; }

        public int? JoinNum { get; set; }

        public int? Numbers { get; set; }

        public int? MaxAge { get; set; }

        public int? MinAge { get; set; }

        [StringLength(300)]
        public string Remark { get; set; }

        [StringLength(200)]
        public string Place { get; set; }

        [StringLength(50)]
        public string Remark01 { get; set; }

        [StringLength(50)]
        public string Remark02 { get; set; }

        [StringLength(50)]
        public string ActivityRunSTime { get; set; }

        [StringLength(50)]
        public string ActivityRunETime { get; set; }
    }
}
