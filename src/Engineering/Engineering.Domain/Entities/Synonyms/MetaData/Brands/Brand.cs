using Engineering.Domain.Entities.Synonyms.MetaData.BrandModels;
using Engineering.Domain.Entities.Synonyms.Warehouse.Groups;
using Engineering.Domain.Entities.Synonyms.Warehouse.Products;

namespace Engineering.Domain.Entities.Synonyms.MetaData.Brands;

public class ViewBrand : ActivateEntity<ViewBrand, long>
{
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public long CompanyId { get; private set; }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private readonly IList<ViewBrandModel> _brandModels;
    public IEnumerable<ViewBrandModel> BrandModels => _brandModels.AsReadOnly();

    private readonly IList<ViewProduct> _products;
    public IEnumerable<ViewProduct> Products => _products.AsReadOnly();

    private readonly IList<ViewGroup> _groups;
    public IEnumerable<ViewGroup> Groups => _groups.AsReadOnly();
    private ViewBrand()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        _brandModels = [];
        _products = [];
        _groups = [];
    }
}