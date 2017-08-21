using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace TravelOnline.Models
{
    public partial class OL_OrderPrice
    {
        public Guid OrderId { get; set; }
        public string PriceType { get; set; }
        public int? PriceId { get; set; }
        public string PriceName { get; set; }
        public string PriceMemo { get; set; }
        [Column(TypeName = "numeric")]
        public decimal? SellPrice { get; set; }
        public byte? OrderNums { get; set; }
        [Column(TypeName = "numeric")]
        public decimal? SumPrice { get; set; }
        public DateTime? InputDate { get; set; }
        [Column(TypeName = "ntext")]
        public string infos { get; set; }
        [Column(TypeName = "ntext")]
        public string guestno { get; set; }
        [Column(TypeName = "ntext")]
        public string guestnostring { get; set; }

    }
}