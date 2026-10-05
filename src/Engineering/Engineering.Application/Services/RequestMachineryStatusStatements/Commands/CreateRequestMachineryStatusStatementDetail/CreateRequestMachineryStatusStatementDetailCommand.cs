using Engineering.Domain.Entities.Machineries;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Commands.CreateRequestMachineryStatusStatementDetail;

public record CreateRequestMachineryStatusStatementDetailCommand(
 RequestMachineryStatusStatement RequestMachineryStatusStatement,
 Project Project,
 Machinery Machinery,
 RequestMachinery RequestMachinery,
 long ContractorId,
 DateTime? FromDate,
 DateTime? ToDate,
 decimal RequestedCount,
 decimal FinalPrice,
 long? CurrencyId,
 RequestMachineryStatusStatementUnit Unit,
 long? OperatorId,
 decimal? TimeRequired,
 List<long>? ProjectOperationIds,
 List<long>? ProjectOperationDetailIds
    ) : ICommand<RequestMachineryStatusStatementDetail>;
