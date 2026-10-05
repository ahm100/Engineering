namespace Engineering.Api.Controllers.RequestGoodsSupplies.Contracts.GetDetailByRGSTypeId;

public record GetDetailByRGSTypeIdExcelRequest(
    long Id,
    List<DetailRGSTypeDetailEnum> ExcelFilters,
    int PageIndex,
    int PageSize) : IHttpRequest;