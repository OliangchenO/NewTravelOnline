namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_UserRight
    {
        public short Id { get; set; }

        [StringLength(50)]
        public string RightName { get; set; }

        [Column(TypeName = "ntext")]
        public string RightCode { get; set; }

        [StringLength(10)]
        public string RightFlag { get; set; }
    }
}
