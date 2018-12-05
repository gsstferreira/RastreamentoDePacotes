using Common.Models;
using System;
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

        public static double Haversine(double lat1, double lng1, double lat2, double lng2)
        {
            int raioTerra = 6371000;
            double pi = Math.PI;

            double rlat1 = (90 - lat1) * pi / 180;
            double rlat2 = (90 - lat2) * pi / 180;
            double deltaLng = (lng1 - lng2) * pi / 180;


            return raioTerra * (Math.Acos(Math.Cos(rlat1) * Math.Cos(rlat2) + Math.Sin(rlat1) * Math.Sin(rlat2) * Math.Cos(deltaLng)));
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
