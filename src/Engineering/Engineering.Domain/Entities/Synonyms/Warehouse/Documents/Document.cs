using Engineering.Domain.Entities.Synonyms.Warehouse.DocumentProducts;
namespace Engineering.Domain.Entities.Synonyms.Warehouse.Documents;

public class ViewDocument : ActivateEntity<ViewDocument>
{
    public string FileUrl { get; private set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private readonly IList<ViewDocumentProduct> _documentProducts;
    public IEnumerable<ViewDocumentProduct> DocumentProducts => _documentProducts.AsReadOnly();

    private ViewDocument()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        _documentProducts = [];
    }

}