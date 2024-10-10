using System.Text.Json;
using System.Text.Json.Serialization;

namespace EmployeeAPI.Converter
{
    public class TickConverter: JsonConverter<TimeSpan?>
    {
     public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.Null)
                {
                    return null;
                }
                if (reader.TokenType == JsonTokenType.StartObject)
                {
                    reader.Read(); // StartObject
                    reader.Read(); // PropertyName "ticks"
                    var ticks = reader.GetInt64();
                    reader.Read(); // EndObject
                    return TimeSpan.FromTicks(ticks);
                }
                throw new JsonException();
            }

            public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();
                writer.WriteNumber("ticks", value?.Ticks ?? 0);
                writer.WriteEndObject();
            }
        }
    
}
