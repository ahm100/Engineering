using Engineering.Domain.Entities.RequestMachineryStatusStatements;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Commands.CreateRequestMachineryStatusStatement;

public record CreateRequestMachineryStatusStatementCommand(
 long ContractorId,
 DateTime? FromDate,
 DateTime? ToDate,
 DateTime? PaymentDate,
 decimal TotalRequestedCount,
 decimal TotalFinalPrice,
 decimal? ContractorPrice,
 long? BankAccountId,
 string? IBAN,
 string? Description,
 long? CostCategoryId,
 long? CostGroupId,
 long? DocumentTypeId,
 long? PreferentialTypeId,
 long? CompanyId
    ) : ICommand<RequestMachineryStatusStatement>;
