using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductManages.Commands.CreateFiduciaryProductDetailManagement;

public record CreateFiduciaryProductDetailManagementCommand(
    long InvoiceId,
    long WarehouseId,
    long? DestWarehouseId,
    int ConfirmedLoanCount,
    string? Description,
    FiduciaryProductDetail FiduciaryProductDetail) : ICommand<FiduciaryProductDetailManagement>;
