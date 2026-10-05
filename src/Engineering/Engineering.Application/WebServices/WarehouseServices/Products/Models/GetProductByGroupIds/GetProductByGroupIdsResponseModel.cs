namespace Engineering.Application.WebServices.WarehouseServices.Products.Models.GetProductByGroupIds;

public record GetProductByGroupIdsResponseModel(
    List<GetProductsByGroupIdsModel> Data,
    int RowCount);

public record GetProductsByGroupIdsModel(
    long Id,
    long GroupId,
    string Name,
    string Code,
    string Desciption,
    string BrandName,
    string BrandModelName,
    bool IsActive,
    bool IsEntryLocked,
    bool IsExitLocked,
    string MeasureUnitTitle,
    long MeasureUnitId,
    double InventoryQuantity,
    bool IsSaleable,
    bool IsPresentable,
    bool HasSerialNo,
    bool IsPerishable,
    bool IsSerialGeneratedAutomatically,
    decimal? Length,
    decimal? Width,
    decimal? Height,
    decimal? Weight,
    decimal? Volume,
    List<string>? Urls,
    List<GetFilteredProductsModel> Alternatives);

public record GetFilteredProductsModel(
    long Id,
    string Name,
    string Code,
    string Brand,
    string BrandModel,
    bool IsActive);