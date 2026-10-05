using Gita.Backend.Shared.Domain.Shared.Attributes;
using System.ComponentModel;
using System.Reflection;

namespace Engineering.Api.Helpers.ExcelTools;

public static class EnumObjectExt
{
    public static List<EnumObjectNew> GetPropertyNamesWithDisplayName<T>()
    {
        List<EnumObjectNew> list = new List<EnumObjectNew>();
        PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
        foreach (PropertyInfo propertyInfo in properties)
        {
            if (propertyInfo.GetCustomAttribute<DisplayNameAttribute>() != null)
            {
                DisplayNameAttribute customAttribute = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();
                ColumnOrderAttribute orderAttribute = propertyInfo.GetCustomAttribute<ColumnOrderAttribute>();
                var dispalyName = string.Empty;
                var order = 0;
                if (customAttribute != null)
                {
                    dispalyName = customAttribute.DisplayName;
                }
                if (orderAttribute != null)
                {
                    order = orderAttribute.Order;
                }
                list.Add(new EnumObjectNew()
                {
                    Code = order,
                    Description = dispalyName,
                    DescriptionEn = propertyInfo.Name,

                });
            }
        }
        list = list.OrderBy(x => x.Code).ToList();
        return list;
    }
}