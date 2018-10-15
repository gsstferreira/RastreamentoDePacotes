using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Common.Services
{
    public abstract class RequestReaderService
    {
        public static JsonObject ReadRequestBody(HttpRequestBase request)
        {
            try
            {
                Stream req = request.InputStream;
                req.Seek(0, System.IO.SeekOrigin.Begin);
                string jsonBody = new StreamReader(req).ReadToEnd();

                return JsonObject.Parse(jsonBody);
            }

            catch(Exception)
            {
                throw new HttpException(400,"Há um erro no corpo da requisição.");
            }
            
        } 

        public class JsonObject
        {
            private JObject json;

            public dynamic GetValueAs<T>(string name)
            {
                T item = json.GetValue(name).ToObject<T>();
                return item;
            }

            public static JsonObject Parse(string json)
            {
                JsonObject j = new JsonObject();
                j.json = JObject.Parse(json);

                return j;
            }
        }

    }
}
