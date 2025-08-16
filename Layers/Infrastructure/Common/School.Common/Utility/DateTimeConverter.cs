using System.Text.Json;
using System.Text.Json.Serialization;

namespace School.Common.Utility;

public class DateTimeConverter : JsonConverter<DateTime>
{
    private readonly string _format;

    public DateTimeConverter(string format) => _format = format;

    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    ) => DateTime.Parse(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        var peruTime = TimeZoneInfo.ConvertTimeFromUtc(
            value,
            TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time")
        );
        writer.WriteStringValue(peruTime.ToString(_format));
    }
}
