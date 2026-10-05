using Engineering.Domain.Entities.Projects.WBS;
namespace Engineering.Application.Services.ProjectWbses.ImportMPP;

public static class TaskOutline
{
    /// Recomputes SortOrder (1..n per parent), OutlineLevel, OutlineNumber, IsSummary.
    /// Pass only non-deleted tasks. Returns the tasks that actually changed.
    public static List<ProjectScheduleTask> Normalize(IReadOnlyCollection<ProjectScheduleTask> tasks)
    {
        var changed = new HashSet<ProjectScheduleTask>();
        var byParent = tasks.ToLookup(t => t.ParentId);

        void Walk(long? parentId, int level, string prefix)
        {
            var i = 0;
            foreach (var t in byParent[parentId].OrderBy(x => x.SortOrder).ThenBy(x => x.Id))
            {
                i++;
                var number = prefix.Length == 0 ? i.ToString() : $"{prefix}.{i}";
                var hasChildren = byParent[t.Id].Any();

                if (t.SortOrder != i || t.OutlineLevel != level || t.OutlineNumber != number)
                {
                    t.SetOutline(level, number, i);
                    changed.Add(t);
                }
                if (t.IsSummary != hasChildren)
                {
                    t.SetIsSummary(hasChildren);
                    changed.Add(t);
                }
                Walk(t.Id, level + 1, number);
            }
        }

        Walk(null, 1, "");
        return changed.ToList();
    }

    /// Depth-first display order (parent, then its children by SortOrder).
    public static List<ProjectScheduleTask> Flatten(IReadOnlyCollection<ProjectScheduleTask> tasks)
    {
        var byParent = tasks.ToLookup(t => t.ParentId);
        var result = new List<ProjectScheduleTask>(tasks.Count);

        void Visit(long? parentId)
        {
            foreach (var t in byParent[parentId].OrderBy(x => x.SortOrder).ThenBy(x => x.Id))
            {
                result.Add(t);
                Visit(t.Id);
            }
        }

        Visit(null);
        return result;
    }
}
