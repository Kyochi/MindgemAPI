using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace MindgemAPI.utils
{
    public class CustomParametersWithWaitHandle
    {
        public AutoResetEvent WaitHandle { get; set; }
        public string dataModel { get; set; }
        public string currFrom { get; set; }
        public string currTo { get; set; }
        public CustomParametersWithWaitHandle(AutoResetEvent h, string dModel, string cFrom, string CTo)
        {
            WaitHandle = h;
            dataModel = dModel;
            currFrom = cFrom;
            currTo = CTo;
        }
    }
}