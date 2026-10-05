using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Queries.GetsProjectOperationDetailDeductionByIds;

public record GetsProjectOperationDetailDeductionByIdsQuery(
    List<long> Ids
    ) : IQuery<List<ProjectOperationDetailDeduction>>;
