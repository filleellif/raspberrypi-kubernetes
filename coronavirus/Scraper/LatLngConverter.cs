using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Scraper
{
    public class LatLngConverter : JsonConverter<LatLng>
    {
        public override LatLng Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            var result = new LatLng();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    return result;
                }

                if (reader.TokenType != JsonTokenType.Number)
                {
                    throw new JsonException();
                }

                result.Latitude = reader.GetDouble();
                reader.Read();
                result.Longitude = reader.GetDouble();
            }
            
            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, LatLng value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
