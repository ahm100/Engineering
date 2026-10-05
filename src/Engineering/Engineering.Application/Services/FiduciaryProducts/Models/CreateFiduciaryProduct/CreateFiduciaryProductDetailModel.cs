namespace Engineering.Application.Services.FiduciaryProducts.Models.CreateFiduciaryProduct;

public record CreateFiduciaryProductDetailModel(long ProductId,
                                                int LoanCount,
                                                int LoanDays,
                                                long MeasureUnitId,
                                                long CurrencyId,
                                                int DailyLateFine);