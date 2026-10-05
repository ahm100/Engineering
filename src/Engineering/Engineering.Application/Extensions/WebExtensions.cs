using System.Web;

namespace Engineering.Application.Extensions;

public static class WebExtensions
{
    public static string GetQueryString(this Dictionary<string, string> obj)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var properties = obj.Select(_ => _.Key + "=" + HttpUtility.UrlEncode(_.Value)).ToList();

        //from p in obj.GetType().GetProperties()
        //where p.GetValue(obj, null) != null
        //select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        var array = properties.ToArray();
        var response = "?" + string.Join("&", array);

        return response;
    }
}
