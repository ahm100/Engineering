using Warehouse.ClientSdks.Enums;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetProductGroupByWareHouseId;

public record GroupsModel(
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
    List<GroupProductsModelNew>? Products
    );


public class GroupClassDto
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}

public record GroupProductsModelNew(
    long Id,
    string Name,
    string Code,
    string? Brand,
    string? BrandModel,
    bool IsActive
    );