namespace TravelOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OL_Invoice
    {
        public int id { get; set; }

        public Guid? OrderId { get; set; }

        [StringLength(50)]
        public string InvoiceName { get; set; }

        [StringLength(50)]
        public string InvoiceContent { get; set; }

        [StringLength(50)]
        public string GuestName { get; set; }

        [StringLength(50)]
        public string GuestMobile { get; set; }

        [StringLength(100)]
        public string GuestAddress { get; set; }

        [StringLength(50)]
        public string InvoiceFlag { get; set; }
    }
}
