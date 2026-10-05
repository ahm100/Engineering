namespace Engineering.Application.Services.FiduciaryProducts.Models.SetFiduciaryProductDetailDelivary;

public record SetFiduciaryProductDetailDelivaryRequest(
    long Id,
    string? ProductDescription) : IHttpRequest;
