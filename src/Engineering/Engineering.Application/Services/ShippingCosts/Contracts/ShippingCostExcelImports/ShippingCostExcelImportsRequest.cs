namespace Engineering.Application.Services.ShippingCosts.Models.ShippingCostExcelImports;

public record ShippingCostExcelImportsRequest(
    long TransportationContractorId,
    IFormFile DocumentFile)
    : IHttpRequest;