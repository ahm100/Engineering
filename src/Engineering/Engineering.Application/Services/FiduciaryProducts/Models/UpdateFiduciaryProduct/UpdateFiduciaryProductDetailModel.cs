namespace Engineering.Application.Services.FiduciaryProducts.Models.UpdateFiduciaryProduct;

public record UpdateFiduciaryProductDetailModel(long? Id,
                                                long ProductId,
                                                int LoanCount,
                                                int LoanDays,
                                                long MeasureUnitId,
                                                long CurrencyId,
                                                int DailyLateFine,
                                                string? Description,
                                                bool IsDeleted);