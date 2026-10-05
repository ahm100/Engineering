using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.OpAssign.GetForUpdate;

public class GetOpAssignForUpdateQueryHandler
    : IQueryHandler<
        GetOpAssignForUpdateQuery,
        ProjectOperationDetailContractorService>
{
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public GetOpAssignForUpdateQueryHandler(
        IProjectOperationDetailContractorServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailContractorService?>> Handle(
        GetOpAssignForUpdateQuery request,
        CT ct)
    {
        var entity =
            await _repository.GetOperationBasedAssignmentForUpdate(
                request.Id,
                ct);

        return entity;
    }
}