using Engineering.Application.Services.ProjectOperations.Models.GetPODate;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetPODate;

public record GetPODateQuery(
    long ProjectOperationId
     ) : IQuery<GetPODateResponse?>;