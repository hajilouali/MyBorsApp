using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace MyBorsApp.ApiControler
{
    public static class Apiservice
    {
        public static async Task<object> GetAPI(string url, string token, object jsonObject)
        {
            string urlget = string.Format("https://bmi.exphoenixfuture.com" + url);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                    var response = await client.GetAsync(urlget);
                    if (response != null)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<object>(jsonString);
                    }
                }
            }
            catch 
            {
                return null;
            }
            return null;
        }
        public static HttpResponseMessage GetMessage(string url, string token)
        {
            string urlget = string.Format("https://bmi.exphoenixfuture.com" + url);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("bearer" , token);
            var respons = client.GetAsync(urlget).Result;
            return respons;
        }

        public static HttpResponseMessage PostMessage(string Url,object ob,string token)
        {
            string urlget = string.Format("https://bmi.exphoenixfuture.com" + Url);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(ob), Encoding.UTF8, "application/json");
            var respons = client.PostAsync(urlget,content).Result;
            return respons;
        }
        public static T fromJson<T>(string json)
        {
            var bytes = Encoding.Unicode.GetBytes(json);

            using (MemoryStream mst = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(mst);
            }
        }

        public static string toJson(object instance)
        {
            using (MemoryStream mst = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(instance.GetType());
                serializer.WriteObject(mst, instance);
                mst.Position = 0;

                using (StreamReader r = new StreamReader(mst))
                {
                    return r.ReadToEnd();
                }
            }
        }
    }
}