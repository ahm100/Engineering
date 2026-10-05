namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSIdReport.Pdf;

public record GetDetailByRGSIdPdfReportRequest(long Id,
    int PageIndex,
    int PageSize) : IHttpRequest;

public record GetDetailByRGSIdPdfEnReportRequest(long Id,
    int PageIndex,
    int PageSize) : IHttpRequest;