
namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailValidator;

public record GetProjectOperationDetailValidatorQuery(
    long ProjectOperationId
    ) : IQuery<bool>;