using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProductManages.Commands.UpdateFiduciaryProductDetailReturn;

public record UpdateFiduciaryProductDetailReturnCommand(string? Description,
                                                        int ReturnCount,
                                                        DateTime ReturnDate,
                                                        int LateDay,
                                                        decimal LateFine,
                                                        long CurrencyId,
                                                        FiduciaryProductDetailReturnType Type,
                                                        long FiduciaryProductDetailReturnId) : ICommand<FiduciaryProductDetailReturn>;
