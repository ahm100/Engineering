using Engineering.Domain.Entities.Synonyms.MetaData.BrandModels;
using Engineering.Domain.Entities.Synonyms.MetaData.Brands;
using Engineering.Domain.Entities.Synonyms.MetaData.MeasureUnits;
using Engineering.Domain.Entities.Synonyms.Warehouse.Categories;
using Engineering.Domain.Entities.Synonyms.Warehouse.DocumentGroups;
using Engineering.Domain.Entities.Synonyms.Warehouse.Products;
using System.ComponentModel.DataAnnotations.Schema;
namespace Engineering.Domain.Entities.Synonyms.Warehouse.Groups;

public class ViewGroup : ActivateEntity<ViewGroup>
{
    public ViewCategory Category { get; private set; }
    public long CategoryId { get; private set; }
    public MeasureUnit MeasureUnit { get; private set; }
    public long MeasureUnitId { get; private set; }
    public ViewBrand? Brand { get; private set; }
    public long? BrandId { get; private set; }
    public ViewBrandModel? BrandModel { get; private set; }
    public long? BrandModelId { get; private set; }
    [ForeignKey("GroupClassId")]
    public long? GroupClassId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string CommercialName { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string? BarCode { get; private set; }
    public string? IranCode { get; private set; }
    public string? QrCode { get; private set; }
    public double? MinimumTemperature { get; private set; }
    public double? MaximumTemperature { get; private set; }
    public bool IsSaleable { get; private set; }
    public bool IsPresentable { get; private set; }
    public bool HasWeightTolerance { get; private set; }
    public bool IsPerishable { get; private set; }
    public bool IsDraft { get; set; }
    public ProductGroupType GroupType { get; private set; }
    public long CompanyId { get; private set; }
    public decimal? Length { get; private set; }
    public decimal? Width { get; private set; }
    public decimal? Height { get; private set; }
    public decimal? Weight { get; private set; }
    public decimal? Volume { get; private set; }
    public string? TechnicalCode { get; private set; }
    public long? LegacyId { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private readonly IList<ViewProduct> _products;
    public IEnumerable<ViewProduct> Products => _products.AsReadOnly();
    private readonly IList<ViewDocumentGroup> _documentGroups;
    public IEnumerable<ViewDocumentGroup> DocumentGroups => _documentGroups.AsReadOnly();
    private ViewGroup()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        _products = [];
        _documentGroups = [];
    }
}

public enum ProductGroupType
{
    [Description("سایر")]
    Etc = 0,

    [Description("محصول نهایی")]
    FinalProduct = 1,

    [Description("محصول نیمه ساخته")]
    SemiProduct = 2,

    [Description("دارایی ثابت")]
    Asset = 3,

    [Description("مواد اولیه")]
    RawMaterial = 4,

    [Description("یدکی")]
    Spare = 5,
}
