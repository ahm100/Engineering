using System.Text.Json;

namespace Engineering.ClientSdk.Services;

public interface IEngineeringClientSdkJsonSerializer
{
    string Serialize(object obj);
}

public class EngineeringClientSdkJsonSerializer : IEngineeringClientSdkJsonSerializer
{
    public static readonly JsonSerializerOptions Options = JsonSerializerOptions.Default;

    public string Serialize(object obj)
    {
        return JsonSerializer.Serialize(obj, Options);
    }
}
