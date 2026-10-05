namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSIdReport.Xslx;

public record GetDetailByRGSIdXslxReportRequest(long Id,
    int PageIndex,
    int PageSize) : IHttpRequest;

public record GetDetailByRGSIdXslxEnReportRequest(long Id,
    int PageIndex,
    int PageSize) : IHttpRequest;