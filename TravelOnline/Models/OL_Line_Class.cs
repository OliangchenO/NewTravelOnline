using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelOnline.Models
{
    public class OL_Line_Class
    {
        public string MisLineId { get; set; }
        public string DestinationName { get; set; }
        public string Price { get; set; }
        public string LineName { get; set; }
    }
}