namespace Engineering.Application.Services.FiduciaryProductManages.Models.SetFiduciaryProductConfirmed;

public record FiduciaryProductDetailModel(long Id,
                                          bool IsRejected,
                                          string? RejectedDescription,
                                          int? ConfirmedLoanDays,
                                          decimal? ConfirmedDailyLateFine,
                                          List<FiduciaryProductDetaiManagementModel>? Warehouses);
