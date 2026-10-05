namespace Engineering.Application.Services.ProjectOperations.Models.UpdatePrice;

public record UpdatePriceRequest(
    long Id,
    decimal? ChangePrice,
    int? IncreaseRate
    ) : IHttpRequest;
