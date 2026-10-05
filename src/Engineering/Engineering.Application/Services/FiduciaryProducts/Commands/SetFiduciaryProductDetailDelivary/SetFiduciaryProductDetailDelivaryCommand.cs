using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProducts.Commands.SetFiduciaryProductDetailDelivary;

public record SetFiduciaryProductDetailDelivaryCommand(
    long FiduciaryProductDetailId,
    string? StatusDescription,
    string? LastDescription
    ) : ICommand<FiduciaryProductDetail>;
