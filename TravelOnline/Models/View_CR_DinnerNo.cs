namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_CR_DinnerNo
    {
        public int id { get; set; }

        public int? Lineid { get; set; }

        [StringLength(20)]
        public string TabelNo { get; set; }

        public int? Berth { get; set; }

        [StringLength(50)]
        public string DinnerTime { get; set; }

        public int? Nums { get; set; }
    }
}
