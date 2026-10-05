using Engineering.Domain.Entities.Synonyms.Warehouse.Documents;
using Engineering.Domain.Entities.Synonyms.Warehouse.Groups;

namespace Engineering.Domain.Entities.Synonyms.Warehouse.DocumentGroups;

public class ViewDocumentGroup : ActivateEntity<ViewDocumentGroup>
{
    public long DocumentId { get; private set; }
    public ViewDocument Document { get; private set; }
    public long GroupId { get; private set; }
    public ViewGroup Group { get; private set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private ViewDocumentGroup()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
}