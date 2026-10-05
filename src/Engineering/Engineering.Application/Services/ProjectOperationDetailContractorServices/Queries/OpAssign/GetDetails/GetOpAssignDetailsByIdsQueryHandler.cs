using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices
    .Queries.OpAssign.GetDetails;

public class GetOpAssignDetailsByIdsQueryHandler
    : IQueryHandler<
        GetOpAssignDetailsByIdsQuery,
        List<ProjectOperationDetail>>
{
    private readonly IProjectOperationDetailRepository _repository;

    public GetOpAssignDetailsByIdsQueryHandler(
        IProjectOperationDetailRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<ProjectOperationDetail>?>> Handle(
        GetOpAssignDetailsByIdsQuery request,
        CT ct)
    {
        var result =
            await _repository.GetsForOperationBasedAssignmentByIds(
                request.ProjectOperationId,
                request.ProjectOperationDetailIds,
                ct);

        return result;
    }
}