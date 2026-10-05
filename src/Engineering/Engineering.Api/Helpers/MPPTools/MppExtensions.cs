using Engineering.Application.Services.ProjectWbses.ImportMPP;

namespace Engineering.Api.Helpers.MPPTools;

public static class MppExtensions
{
    public static string GetIndentedName(this MppTaskModel task)
    {
        if (task.OutlineLevel <= 1)
            return task.Name;

        var indent = new string(' ', (task.OutlineLevel - 1) * 4);
        return $"{indent}{task.Name}";
    }

    public static string GetOutlineNumber(this MppTaskModel task)
    {
        return task.OutlineNumber ?? string.Empty;
    }

    public static bool IsTopLevel(this MppTaskModel task) => task.OutlineLevel <= 1;
    public static bool IsMidLevel(this MppTaskModel task) => task.OutlineLevel is 2 or 3;
    public static bool IsLeafTask(this MppTaskModel task) => task.OutlineLevel > 3;
}