using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace Engineering.Api.Helpers.Attributes
{
    public class DescriptionOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var descriptionAttr = context.MethodInfo
                .GetCustomAttribute<DescriptionAttribute>();

            if (descriptionAttr is null) return;

            operation.Description = descriptionAttr.Description;
        }
    }
}