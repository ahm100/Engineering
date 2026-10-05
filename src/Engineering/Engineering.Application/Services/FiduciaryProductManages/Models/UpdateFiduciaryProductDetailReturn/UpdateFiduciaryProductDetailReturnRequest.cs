using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProductManages.Models.UpdateFiduciaryProductDetailReturn;

public record UpdateFiduciaryProductDetailReturnRequest(string? Description,
                                                        int ReturnCount,
                                                        DateTime ReturnDate,
                                                        int LateDay,
                                                        decimal LateFine,
                                                        long CurrencyId,
                                                        FiduciaryProductDetailReturnType Type,
                                                        long FiduciaryProductDetailReturnId,
                                                        List<string>? Documents) : IHttpRequest;
