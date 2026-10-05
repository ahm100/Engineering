using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductDetails.Commands.CreateFiduciaryProductDetail;

public record CreateFiduciaryProductDetailCommand(FiduciaryProduct FiduciaryProduct,
                                                  long ProductId,
                                                  int LoanCount,
                                                  int LoanDays,
                                                  long MeasureUnitId,
                                                  long CurrencyId,
                                                  int DailyLateFine) : ICommand<FiduciaryProductDetail>;
