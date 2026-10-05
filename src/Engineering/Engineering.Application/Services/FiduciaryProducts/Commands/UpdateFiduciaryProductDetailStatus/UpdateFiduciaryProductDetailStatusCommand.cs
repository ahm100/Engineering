using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProducts.Commands.UpdateFiduciaryProductDetailStatus;

public record UpdateFiduciaryProductDetailStatusCommand(
    long FiduciaryProductDetailId,
    FiduciaryProductDetailStatus Status,
    string? StatusDescription,
    string? LastDescription) : ICommand<FiduciaryProductDetail>;
