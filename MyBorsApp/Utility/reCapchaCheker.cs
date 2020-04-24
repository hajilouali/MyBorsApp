using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Partosazancnc.Models.ViewModels;

namespace MyBorsApp.Tools
{
    public static class reCapchaCheker
    {
        public static bool isValid(string key)
        {
            string urlToPost = "https://www.google.com/recaptcha/api/siteverify";
            string secretKey = ConfigurationManager.AppSettings["RecapchaSecretKey"]; 
            string gRecaptchaResponse = key;

            var postData = "secret=" + secretKey + "&response=" + gRecaptchaResponse;

            // send post data
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlToPost);
            request.Method = "POST";
            request.ContentLength = postData.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(postData);
            }

            // receive the response now
            string result = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }

            // validate the response from Google reCaptcha
            var captChaesponse = JsonConvert.DeserializeObject<ReCaptcha.reCaptchaResponse>(result);
            if (captChaesponse.Success)
            {
                return true;
            }

            return false;
        }
    }
}