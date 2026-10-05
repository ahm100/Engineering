using Engineering.Domain.Entities.Synonyms.MetaData.Brands;
using Engineering.Domain.Entities.Synonyms.Warehouse.Groups;
using Engineering.Domain.Entities.Synonyms.Warehouse.Products;

namespace Engineering.Domain.Entities.Synonyms.MetaData.BrandModels;

public class ViewBrandModel : ActivateEntity<ViewBrandModel, long>
{

    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public long CompanyId { get; private set; }
    public ViewBrand Brand { get; private set; }
    public long BrandId { get; private set; }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private readonly IList<ViewProduct> _products;
    public IEnumerable<ViewProduct> Products => _products.AsReadOnly();

    private readonly IList<ViewGroup> _groups;
    public IEnumerable<ViewGroup> Groups => _groups.AsReadOnly();
    private ViewBrandModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        _products = [];
        _groups = [];
    }
}
