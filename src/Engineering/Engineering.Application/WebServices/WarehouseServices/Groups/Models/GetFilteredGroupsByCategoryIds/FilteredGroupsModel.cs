namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByCategoryIds;

public record FilteredGroupsModel(
    long Id,
    string? Name,
    string? CommercialName,
    string? Code,
    List<string>? Urls,
    string? Description,
    string? BarCode,
    string? IranCode,
    string? QrCode,
    long? CategoryId,
    string? CategoryCode,
    double? MinimumTemperature,
    double? MaximumTemperature,
    long? MeasureUnitId,
    string? MeasureUnitName,
    bool? IsSaleable,
    bool? IsPresentable,
    bool? HasSerialNo,
    bool? IsPerishable,
    bool? IsActive,
    bool? IsSerialGeneratedAutomatically,
    long? BrandId,
    long? BrandModelId,
    List<GroupProductsModel>? Products
    );

public record GroupProductsModel(
    long Id,
    string? Name,
    string? Code,
    string? Brand,
    string? BrandModel,
    bool IsActive);