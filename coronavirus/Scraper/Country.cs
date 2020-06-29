using System;
using System.Text.Json.Serialization;

namespace Scraper
{
    public class Country
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("alpha2Code")]
        public string Code1 { get; set; }
        
        [JsonPropertyName("alpha3Code")]
        public string Code2 { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("subregion")]
        public string SubRegion { get; set; }

        [JsonPropertyName("population")]
        public int Population { get; set; }

        [JsonPropertyName("capital")]
        public string Capital { get; set; }

        [JsonPropertyName("latlng")]
        [JsonConverter(typeof(LatLngConverter))]
        public LatLng LatLng 
        {
            get 
            {
                if (!Latitude.HasValue || !Longitude.HasValue)
                {
                    throw new Exception();
                }
                return new LatLng { Longitude = Longitude.Value, Latitude = Latitude.Value };
            }
            set
            {
                Latitude = value.Latitude;
                Longitude = value.Longitude;
            }
        }

        public double? Latitude { get; set; }
        
        public double? Longitude { get; set; }

        [JsonPropertyName("flag")]
        public string FlagUrl { get; set; }
    }
}
