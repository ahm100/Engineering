using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProductManages.Commands.CreateFiduciaryProductDetailReturn;

public record CreateFiduciaryProductDetailReturnCommand(long InvoiceId,
                                                        string? Description,
                                                        int ReturnCount,
                                                        DateTime ReturnDate,
                                                        int LateDay,
                                                        decimal LateFine,
                                                        long CurrencyId,
                                                        FiduciaryProductDetailReturnType Type,
                                                        FiduciaryProductDetailManagement FiduciaryProductDetailManagement) : ICommand<FiduciaryProductDetailReturn>;
