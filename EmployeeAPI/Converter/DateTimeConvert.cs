using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EmployeeAPI.Converter
{
    public class DateTimeConvert:JsonConverter<DateTime>
    {
        private string Formatdate = "dd/MM/yyyy";
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), Formatdate,
                CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Formatdate));
        }
    }
}
