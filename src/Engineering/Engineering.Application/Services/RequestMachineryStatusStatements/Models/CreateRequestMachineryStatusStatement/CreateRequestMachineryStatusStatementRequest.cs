using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Models.CreateRequestMachineryStatusStatement;

public record CreateRequestMachineryStatusStatementRequest(
 List<long>? Ids,
 List<long>? CostCenterIds,
 List<long>? ProjectIds,
 long ContractorId,
 List<long>? ProjectOperationIds,
 List<long>? ProjectOperationDetailIds,
 List<long>? MachineryIds,
 RequestMachineryUnit? Unit,
 List<RequestMachineryStatus>? Statuses,
 DateTime? StartDate,
 DateTime? EndDate,
 DateTime? ConfirmedFromDate,
 DateTime? ConfirmedToDate,
 string? FilterData,
 decimal? ContractorPrice,
 string? IBAN,
 string? Description,
 DateTime? PaymentDate,
 long? BankAccountId,
 long? SeasonId,
 long? CostCategoryId,
 long? CostGroupId,
 long? DocumentTypeId,
 long? PreferentialTypeId,
 string? PettyCashId,
 bool IsPettyCash = false) : IHttpRequest;
