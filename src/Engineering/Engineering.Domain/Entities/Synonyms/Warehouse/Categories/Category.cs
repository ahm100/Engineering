using Engineering.Domain.Entities.Synonyms.Warehouse.Groups;

namespace Engineering.Domain.Entities.Synonyms.Warehouse.Categories;

public class ViewCategory : ActivateEntity<ViewCategory>
{
    public int Level { get; private set; }
    public long? ParentId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public int SortPriority { get; private set; }
    public long CompanyId { get; private set; }
    public Category? Parent { get; private set; }
    public long? LegacyId { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private readonly IList<ViewGroup> _groups;
    public IEnumerable<ViewGroup> Groups => _groups.AsReadOnly();
    private ViewCategory()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        _groups = [];
    }
}
