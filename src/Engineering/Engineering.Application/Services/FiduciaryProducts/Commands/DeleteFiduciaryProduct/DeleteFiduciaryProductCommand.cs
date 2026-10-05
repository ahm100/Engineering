using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProducts.Commands.DeleteFiduciaryProduct;

public record DeleteFiduciaryProductCommand(long FiduciaryProductId) : ICommand<FiduciaryProduct>;
