using Engineering.Domain.Entities.Synonyms.Warehouse.Documents;
using Engineering.Domain.Entities.Synonyms.Warehouse.Products;

namespace Engineering.Domain.Entities.Synonyms.Warehouse.DocumentProducts;

public class ViewDocumentProduct : ActivateEntity<ViewDocumentProduct>
{

    public long DocumentId { get; private set; }
    public ViewDocument Document { get; private set; }

    public long ProductId { get; private set; }
    public ViewProduct Product { get; private set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private ViewDocumentProduct()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
}