using Engineering.Domain.Entities.Synonyms.MetaData.BrandModels;
using Engineering.Domain.Entities.Synonyms.MetaData.Brands;
using Engineering.Domain.Entities.Synonyms.Warehouse.Groups;

namespace Engineering.Domain.Entities.Synonyms.Warehouse.Products;

public class ViewProduct : ActivateEntity<ViewProduct, long>
{
    public ViewGroup Group { get; private set; }
    public long GroupId { get; private set; }
    public ViewBrand? Brand { get; private set; }
    public long? BrandId { get; private set; }
    public ViewBrandModel? BrandModel { get; private set; }
    public long? BrandModelId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? NameEn { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public Guid? PreferentialReferenceCode { get; private set; } = Guid.NewGuid();
    public long CompanyId { get; private set; }
    public decimal? Length { get; private set; }
    public decimal? Width { get; private set; }
    public decimal? Height { get; private set; }
    public decimal? Weight { get; private set; }
    public decimal? Volume { get; private set; }
    public long? LegacyId { get; private set; }
    public string? Description { get; private set; }
    public string? DescriptionEn { get; private set; }
    public string? TechnicalCode { get; private set; }

    private ViewProduct()
    {

    }
}