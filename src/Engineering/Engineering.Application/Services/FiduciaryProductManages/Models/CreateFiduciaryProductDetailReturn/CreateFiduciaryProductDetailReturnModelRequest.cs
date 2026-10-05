using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProductManages.Models.CreateFiduciaryProductDetailReturn;

public record CreateFiduciaryProductDetailReturnModelRequest(long Id,
                                                             string? Description,
                                                             int ReturnCount,
                                                             DateTime ReturnDate,
                                                             int LateDay,
                                                             decimal LateFine,
                                                             long CurrencyId,
                                                             FiduciaryProductDetailReturnType Type,
                                                             List<string>? Documents);