using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProducts.Commands.DeleteFiduciaryProductDetail;

public record DeleteFiduciaryProductDetailCommand(long FiduciaryProductDetailId) : ICommand<FiduciaryProductDetail>;
