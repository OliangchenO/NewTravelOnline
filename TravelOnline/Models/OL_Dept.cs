namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_Dept
    {
        public short Id { get; set; }

        [StringLength(50)]
        public string DeptName { get; set; }

        public int? ErpId { get; set; }
    }
}
