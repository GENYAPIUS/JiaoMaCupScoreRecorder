using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JiaoMaCupScoreRecorder.Client.Utils;

public static class JsonSerializerUtils
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = GetJsonSerializerOptions();

    public static string Serialize<T>(T value)
    {
        var result = JsonSerializer.Serialize(value, _jsonSerializerOptions);

        return result;
    }

    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}