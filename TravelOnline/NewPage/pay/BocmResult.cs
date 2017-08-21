using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.NewPage.pay
{
    public class BocmResult
    {
        
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public string errorCode { get; set; }
        public string errorMsg { get; set; }
        public string hostDate { get; set; }
        public string hostTime { get; set; }
        public string authNo { get; set; }
        public string fee { get; set; }
        public string codeSeq { get; set; }
        public string last4No { get; set; }
    }
}