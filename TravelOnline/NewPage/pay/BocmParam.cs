using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.NewPage.pay
{
    public class BocmParam
    {
        public string cardNo { get; set; }
        public string expireDate { get; set; }
        public string batchNo { get; set; }
        public string voucherNo { get; set; }
        public string traceNo { get; set; }
        public string customerName { get; set; }
        public string codeSeq { get; set; }
        public string code { get; set; }
        public string oldBatchNo { get; set; }
        public string oldVoucherNo { get; set; }
    }
}