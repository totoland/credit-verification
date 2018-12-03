using Newtonsoft.Json;
using System;

namespace CardVerifyCore.Models
{
    public class ServerResponse 
    {
        internal static ServerResponse OK = new ServerResponse { RespCode = 200};
        internal static ServerResponse ERROR = new ServerResponse { RespCode = 500 };
        internal static ServerResponse BadRequest = new ServerResponse { RespCode = 400 };
        internal static ServerResponse Unauthorized = new ServerResponse { RespCode = 401 };
        internal static ServerResponse Forbidden = new ServerResponse { RespCode = 403 };
        internal static ServerResponse NotFound = new ServerResponse { RespCode = 404 };

        public int RespCode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RespDesc { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Result { get; set; }
    }
}