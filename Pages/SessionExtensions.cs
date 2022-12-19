using Shoe_Store_Application.Pages.Product;
using System.Text.Json;
using System.Text.Json.Serialization;

public static class SessionExtensions
{
    public static void Set(this ISession session, string key, object value)
    {
        Console.WriteLine(JsonSerializer.Serialize(value));
        session.SetString(key, JsonSerializer.Serialize(value));
        
    }

    public static T Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        if (value == null) return default(T);
        return JsonSerializer.Deserialize<T>(value);
    }
}