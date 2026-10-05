namespace Engineering.Application.Services.FiduciaryProductManages.Models.SetFiduciaryProductConfirmed;

public record FiduciaryProductDetaiManagementModel(
    long WarehouseId,
    long? DestWarehouseId,
    int ConfirmedLoanCount);