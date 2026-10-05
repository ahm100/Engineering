using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Extensions.MPPTools;

public static class TaskOutlineRecalculator
{
    public static void Recalculate(IReadOnlyList<ProjectScheduleTask> allTasksInSchedule)
    {
        var childrenByParent = allTasksInSchedule.ToLookup(t => t.ParentId);

        var sortCounter = 0;

        void Walk(long? parentId, string parentOutlineNumber, int level)
        {
            var siblings = childrenByParent[parentId].OrderBy(t => t.Id).ToList();

            for (var i = 0; i < siblings.Count; i++)
            {
                var task = siblings[i];
                var outlineNumber = string.IsNullOrEmpty(parentOutlineNumber)
                    ? (i + 1).ToString()
                    : $"{parentOutlineNumber}.{i + 1}";

                task.SetOutline(level, outlineNumber, sortCounter++);

                Walk(task.Id, outlineNumber, level + 1);
            }
        }

        Walk(null, string.Empty, 0);
    }
}