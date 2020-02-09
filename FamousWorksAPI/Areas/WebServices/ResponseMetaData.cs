using System;

namespace FamousWorksAPI.Areas.WebServices
{
    [Serializable]
    public class ResponseMetaData
    {
        public String message { get; set; }
        public int errorCode { get; set; }

        public ResponseMetaData(string message, int errorCode)
        {
            this.message = message;
            this.errorCode = errorCode;
        }
    }
}