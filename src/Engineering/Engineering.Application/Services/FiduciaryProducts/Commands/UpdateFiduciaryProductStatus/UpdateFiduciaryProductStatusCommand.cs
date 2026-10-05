using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProducts.Commands.UpdateFiduciaryProductStatus;

public record UpdateFiduciaryProductStatusCommand(long FiduciaryProductId,
                                                  FiduciaryProductStatus Status,
                                                  string? StatusDescription,
                                                  string? LastDescripiton) : ICommand<FiduciaryProduct>;
