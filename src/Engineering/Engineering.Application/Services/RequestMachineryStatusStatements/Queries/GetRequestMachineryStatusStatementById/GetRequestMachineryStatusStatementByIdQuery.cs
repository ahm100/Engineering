using Engineering.Domain.Entities.RequestMachineryStatusStatements;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Queries.GetRequestMachineryStatusStatementById;

public record GetRequestMachineryStatusStatementByIdQuery(
    long Id
    ) : IQuery<RequestMachineryStatusStatement>;
