namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Company")]
    public partial class Company
    {
        public int id { get; set; }

        public Guid? uid { get; set; }

        [StringLength(100)]
        public string companyname { get; set; }

        [StringLength(50)]
        public string sortpy { get; set; }

        [StringLength(100)]
        public string address { get; set; }

        [StringLength(50)]
        public string zipcode { get; set; }

        [StringLength(50)]
        public string area { get; set; }

        [StringLength(50)]
        public string city { get; set; }

        [StringLength(50)]
        public string tel { get; set; }

        [StringLength(50)]
        public string fax { get; set; }

        public int? misid { get; set; }

        [StringLength(1)]
        public string RebateFlag { get; set; }

        public int? TypeFlag { get; set; }
    }
}
