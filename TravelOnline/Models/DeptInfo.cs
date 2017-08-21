namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DeptInfo")]
    public partial class DeptInfo
    {
        public int id { get; set; }

        public Guid? uid { get; set; }

        public Guid? companyid { get; set; }

        [StringLength(50)]
        public string deptname { get; set; }

        [StringLength(50)]
        public string depttel { get; set; }

        [StringLength(50)]
        public string deptfax { get; set; }

        public int? misid { get; set; }
    }
}
