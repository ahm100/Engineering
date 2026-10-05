using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Api.Controllers.RequestGoodsSupplies.Contracts.GetFltrRGS;

public record GetFltrRGSExcelRequest(
    List<long>? ProjectIds,
    long? CityId,
    long? ProjectManagerId,
    List<GoodsSupplyStatus>? Statuses,
    List<GoodsSupplyType>? Types,
    List<FltrRGSEnum> ExcelFilters,
    List<long>? CreatorIds,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
