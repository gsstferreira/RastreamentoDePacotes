using Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Common.Services
{
    public class GeocodingService
    {
        private static readonly string google_url = "https://maps.googleapis.com/maps/api/geocode/json?";
        private static readonly string api_key = "AIzaSyDLZI5N0ja40O0Ix18QdbULbBMF3ViwxDk";
        private static readonly HttpClient client = new HttpClient();
        private static readonly JavaScriptSerializer serializer = new JavaScriptSerializer(); 

        public static LatLng obterCoordenadas(Endereco endereco)
        {
            string addr = "address=" + endereco.Logradouro + "+" + endereco.Numero + "+" + endereco.Bairro + "+" + endereco.Municipio;
            string key = "key=" + api_key;

            string url = google_url + addr + "&" + key;

            var responseString = client.GetStringAsync(url);
            responseString.Wait();

            var response = serializer.Deserialize<GeocodingResponse>(responseString.Result);

            if(response.status.Equals("OK"))
            {
                var coords = new LatLng
                {
                    Latitude = response.results.FirstOrDefault().geometry.location.lat,
                    Longitude = response.results.FirstOrDefault().geometry.location.lng
                };

                return coords;
            }

            return new LatLng();
        }

        private class GeocodingResponse
        {
            public string status { get; set; }
            public List<GeocodingResult> results { get; set; }
        }

        private class GeocodingResult
        {
            public string formatted_address { get; set; }
            public Geometry geometry { get; set; }
        }

        private class Geometry
        {
            public Location location { get; set; }
        }

        private class Location
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }
    }
}
