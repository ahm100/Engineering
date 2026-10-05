using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductDetails.Commands.UpdateConfirmedFiduciaryProductDetail;

public record UpdateConfirmedFiduciaryProductDetailCommand(long FiduciaryProductDetailId,
                                                           decimal? ConfirmedDailyLateFine,
                                                           int? ConfirmLoanDays) : ICommand<FiduciaryProductDetail>;
