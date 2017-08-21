using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;

namespace TravelOnline.Class.Common
{
    public class CMBC_DLL
    {
        [DllImport("FirmClient.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short CheckInfoFromBank(string pszPublickeyFilePath, string pszMsg);

        [DllImport("FirmClient.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern string GetLastErr(short isNo);
    }
}