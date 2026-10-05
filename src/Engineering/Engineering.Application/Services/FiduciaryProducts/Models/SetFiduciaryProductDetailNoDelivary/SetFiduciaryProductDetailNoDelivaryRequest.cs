namespace Engineering.Application.Services.FiduciaryProducts.Models.SetFiduciaryProductDetailNoDelivary;

public record SetFiduciaryProductDetailNoDelivaryRequest(
    long Id,
    string? ProductDescription) : IHttpRequest;