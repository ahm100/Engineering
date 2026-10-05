namespace Engineering.Api.Controllers.RequestGoodsSupplies.Contracts.GetRGSTypeByRGSId;

public record GetRGSTypeByRGSIdExcelRequest(
    long Id,
    List<RGSTypeEnum> ExcelFilters,
    int PageIndex,
    int PageSize) : IHttpRequest;