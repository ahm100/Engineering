namespace Engineering.Application.Services.TransportationContractors.Models.PriceWeightExcelImports;

public record PriceWeightExcelImportsRequest(
    long TransportationContractorId,
    IFormFile DocumentFile)
    : IHttpRequest;