using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamousWorksAPI.Areas.WebServices
{
    [Serializable]
    public class WSResponse
    {
        public Object data { get; set; }
        public ResponseMetaData metaData { get; set; }

        public WSResponse(string message, int errorCode, object data)
        {
            this.metaData = new ResponseMetaData(message, errorCode);
            this.data = data;
        }
    }
}