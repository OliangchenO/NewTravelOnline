using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace TravelOnline.Models
{
    public partial class OL_CuisesRoom
    {
        public Guid OrderId { get; set; }
        public int? PriceId { get; set; }
        public int? CuisesRoomId { get; set; }
        public string RoomName { get; set; }
        public int? BedNum { get; set; }
        public int? RoomNum { get; set; }
        public int? OrderNum { get; set; }
        public int? AdultNum { get; set; }
        public int? ChildNum { get; set; }
        [Column(TypeName = "numeric")]
        public decimal? price { get; set; }
        public DateTime? InputDate { get; set; }

    }
}