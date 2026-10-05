using Aspose.Tasks;
using Engineering.Domain.Entities.Projects.Enums;
using System.Globalization;

namespace Engineering.Api.Helpers.MPPTools;

public static class MppCustomFieldSlots
{
    public static (string Code, string FieldId) Slot(ProjectScheduleColumnDataType type, int index)
    {
        var (prefix, first) = type switch
        {
            ProjectScheduleColumnDataType.Decimal => ("Number", 1),
            ProjectScheduleColumnDataType.DateTime => ("Start", 1),
            _ => ("Text", 2)     // Text1 holds the task UID
        };

        var code = $"{prefix}{first + index}";
        var fieldId = ((int)Enum.Parse<ExtendedAttributeTask>(code)).ToString(CultureInfo.InvariantCulture);
        return (code, fieldId);
    }
}