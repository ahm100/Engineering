namespace Engineering.Application.Services.FiduciaryProductManages.Models.SetFiduciaryProductConfirmed;

public record SetFiduciaryProductConfirmedRequest(
    long Id,
    string? ProductDescription,
    List<FiduciaryProductDetailModel>? Details) : IHttpRequest;
