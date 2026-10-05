using Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.CreatePaymentOrderWithAutoDetail;
using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.Seasons;

namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.CreatePaymentOrderWithAutoDetail;

public record CreatePaymentOrderWithAutoDetailCommand(
        ContractorStatusStatement StatusStatement,
        Season Season,
        ThirdPartyByIdModel? ThirdParty,
        decimal? ConfirmedPrice,
        long? ConfirmedBankAccountId,
        DateTime? ConfirmedPaymentDate,
        List<string>? Urls,
        string? Description,
        long? CostCategoryId,
        long? CostGroupId,
        long? DocumentTypeId,
        long? PreferentialTypeId,
        ManagerDataModel? PrimaryManager,
        ManagerDataModel? FinalManager
    ) : ICommand<CreatePaymentOrderWithAutoDetailResponseModel?>;
