using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Queries.GetProjectOperationDetailDeductionById;

public record GetProjectOperationDetailDeductionByIdQuery(
    long Id
    ) : IQuery<ProjectOperationDetailDeduction?>;