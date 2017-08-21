namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class wz_dingzhi
    {
        public int id { get; set; }

        [StringLength(50)]
        public string lxr { get; set; }

        [StringLength(10)]
        public string xb { get; set; }

        [StringLength(50)]
        public string lxdh { get; set; }

        [StringLength(100)]
        public string cyyx { get; set; }

        [StringLength(50)]
        public string lxsh { get; set; }

        [StringLength(100)]
        public string cymmd { get; set; }

        [StringLength(50)]
        public string cfrq { get; set; }

        [StringLength(50)]
        public string xcts { get; set; }

        [StringLength(10)]
        public string cr { get; set; }

        [StringLength(10)]
        public string et { get; set; }

        [StringLength(10)]
        public string yr { get; set; }

        [StringLength(50)]
        public string yjfy { get; set; }

        [StringLength(50)]
        public string hzqfd { get; set; }

        [StringLength(10)]
        public string wfjt_fj { get; set; }

        [StringLength(10)]
        public string wfjt_hc { get; set; }

        [StringLength(10)]
        public string wfjt_yl { get; set; }

        [StringLength(10)]
        public string wfjt_qc { get; set; }

        [StringLength(10)]
        public string zsbz { get; set; }

        [StringLength(500)]
        public string gdxq { get; set; }

        public DateTime? inputtime { get; set; }

        [StringLength(1)]
        public string flag { get; set; }
    }
}
