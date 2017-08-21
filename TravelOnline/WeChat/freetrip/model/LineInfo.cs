using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class LineInfo
    {
        public string Id { get; set; }
        public int MisLineId { get; set; }
        public string LineType { get; set; }
        public string LineName { get; set; }
        public int LineClass { get; set; }
        public int LineDays { get; set; }
        public string Standard { get; set; }
        public int Topic { get; set; }
        public decimal Price { get; set; }
        public string AreaId { get; set; }
        public string LineFeature { get; set; }
        public DateTime PlanDate { get; set; }
        public int Preferences { get; set; }
        public int Recommend { get; set; }
        public int LineSort { get; set; }
        public int Ranking { get; set; }
        public int PV { get; set; }
        public int Sale { get; set; }
        public DateTime EditTime { get; set; }
        public int DeptId { get; set; }
        public string Pics { get; set; }
        public string Pdates { get; set; }
        public string SpFlag { get; set; }
        public decimal msprice { get; set; }
        public decimal yfk { get; set; }
        public string Tags { get; set; }
        public int CuisesId { get; set; }
        public int SfId { get; set; }
        public int wwwyh { get; set; }
        public string VisaId { get; set; }
        public int Shipid { get; set; }
        public int Planid { get; set; }
        public string dinner { get; set; }
        public int AgeLimit { get; set; }
        public string VisitSell { get; set; }
        public string StateFlag { get; set; }
        public string CruisesReport { get; set; }
        public string Destinationid { get; set; }
        public string Destination { get; set; }
        public string DestinationList { get; set; }
        public int FirstDestination { get; set; }
        public int IndexRecom { get; set; }
        public int NewRecom { get; set; }
        public string NewSortTime { get; set; }
        public string BigPics { get; set; }
        public string famous { get; set; }
        public string viewids { get; set; }
        public string viewlist { get; set; }
        public string viewname { get; set; }
        public string Integral { get; set; }
        public int WeChat { get; set; }
        public DateTime WeChatSortTime { get; set; }
        public DateTime TopBegin { get; set; }
        public DateTime TopEnd { get; set; }
        public string PlanType { get; set; }
    }
}