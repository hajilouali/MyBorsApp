using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Partosazancnc.Models.ViewModels
{
    public class ReCaptcha
    {
        public class reCaptchaResponse
        {
            [JsonProperty("success")] public bool Success { get; set; }

            [JsonProperty("challenge_ts")] public string ValidatedDateTime { get; set; }

            [JsonProperty("hostname")] public string HostName { get; set; }

            [JsonProperty("error-codes")] public List<string> ErrorCodes { get; set; }
        }
    }
}