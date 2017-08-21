namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_ThirdRoomCheck
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        public Guid? OrderId { get; set; }

        public int? lineid { get; set; }

        public int? berth { get; set; }

        [StringLength(1)]
        public string orderflag { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int nums { get; set; }
    }
}
