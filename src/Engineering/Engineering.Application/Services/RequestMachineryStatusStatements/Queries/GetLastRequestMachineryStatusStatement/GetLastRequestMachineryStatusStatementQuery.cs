using Engineering.Domain.Entities.RequestMachineryStatusStatements;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Queries.GetLastRequestMachineryStatusStatement;

public record GetLastRequestMachineryStatusStatementQuery(
        long ContractorId,
        long? CompanyId
    ) : IQuery<RequestMachineryStatusStatement>;
