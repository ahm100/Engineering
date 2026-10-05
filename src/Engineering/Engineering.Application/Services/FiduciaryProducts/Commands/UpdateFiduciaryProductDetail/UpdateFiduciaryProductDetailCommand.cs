using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductDetails.Commands.UpdateFiduciaryProductDetail;

public record UpdateFiduciaryProductDetailCommand(FiduciaryProductDetail FiduciaryProductDetail,
                                                  long ProductId,
                                                  int LoanCount,
                                                  int LoanDays,
                                                  long MeasureUnitId,
                                                  long CurrencyId,
                                                  int DailyLateFine,
                                                  string? Description) : ICommand<FiduciaryProductDetail>;
