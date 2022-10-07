using Newtonsoft.Json;

namespace Fitness.Common.Extensions;

public static class Serialize
{
    public static byte[]? DataSerialize<T>(this T data)
    {
        if (data == null) return null;

        var json = JsonConvert.SerializeObject(data);
        var bytes = Encoding.UTF8.GetBytes(json);
        return bytes;
    }

    public static T? Deserialize<T>(this byte[]? bytes)
    {
        if (bytes == null) return default;
        var result = Encoding.UTF8.GetString(bytes);
        return JsonConvert.DeserializeObject<T>(result)!;
    }
}