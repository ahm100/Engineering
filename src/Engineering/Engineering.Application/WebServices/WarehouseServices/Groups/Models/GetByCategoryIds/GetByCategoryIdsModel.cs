using Warehouse.ClientSdks.Enums;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetByCategoryIds;

public record GetFilteredGroupsModel(
    long Id,
    string Name,
    string CommercialName,
    string Code,
    string TechnicalCode,
    string Description,
    string BarCode,
    string IranCode,
    string QrCode,
    long CategoryId,
    string CategoryCode,
    string CategoryTitle,
    double MinimumTemperature,
    double MaximumTemperature,
    long MeasureUnitId,
    string MeasureUnitName,
    bool IsSaleable,
    bool IsPresentable,
    bool HasSerialNo,
    bool HasWeightTolerance,
    bool IsPerishable,
    bool IsActive,
    bool? IsSerialGeneratedAutomatically,
    long? BrandId,
    long? BrandModelId,
    long CreatorId,
    long UpdaterId,
    string CreatorFullName,
    string UpdaterFullName,
    ProductGroupType GroupType,
    string GroupTypeTitle,
    long? GroupClassId,
    GroupClassDto? GroupClass,
    List<GroupProductsModel>? Products);

public class GroupClassDto
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string PricingTypeTitle { get; set; } = string.Empty;
}

public class GetFilteredGroupsModelDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CommercialName { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string BarCode { get; set; } = string.Empty;
    public string IranCode { get; set; } = string.Empty;
    public string QrCode { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public string CategoryCode { get; set; } = string.Empty;
    public string CategoryTitle { get; set; } = string.Empty;
    public double MinimumTemperature { get; set; }
    public double MaximumTemperature { get; set; }
    public long MeasureUnitId { get; set; }
    public string MeasureUnitName { get; set; } = string.Empty;
    public bool IsSaleable { get; set; }
    public bool IsPresentable { get; set; }
    public bool HasSerialNo { get; set; }
    public bool HasWeightTolerance { get; set; }
    public bool IsPerishable { get; set; }
    public bool IsActive { get; set; }
    public bool? IsSerialGeneratedAutomatically { get; set; }
    public long? BrandId { get; set; }
    public long? BrandModelId { get; set; }
    public long CreatorId { get; set; }
    public long UpdaterId { get; set; }
    public string CreatorFullName { get; set; } = string.Empty;
    public string UpdaterFullName { get; set; } = string.Empty;
    public ProductGroupType GroupType { get; set; }
    public long? GroupClassId { get; set; }
    public string GroupTypeTitle { get; set; } = string.Empty;
    public List<GroupProductsModel>? Products { get; set; }

}

public record GroupProductsModel(
    long Id,
    string Name,
    string Code,
    string? Brand,
    string? BrandModel,
    bool IsActive
    );
