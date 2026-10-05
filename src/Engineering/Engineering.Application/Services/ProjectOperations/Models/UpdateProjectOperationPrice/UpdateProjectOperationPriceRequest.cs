
namespace Engineering.Application.Services.ProjectOperations.Models.UpdateProjectOperationPrice;

public record UpdateProjectOperationPriceRequest(
    long Id,
    decimal? Price
     ) : IHttpRequest;
