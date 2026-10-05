using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetailContractorServices
    .Models.OpAssign.GetAssignable;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices
    .Queries.OpAssign.GetAssignable;

public class GetAssignablePODsQueryHandler
    : IQueryHandler<
        GetAssignablePODsQuery,
        DataResult<List<AssignablePODModel>>>
{
    private readonly IProjectOperationDetailRepository _repository;

    public GetAssignablePODsQueryHandler(
        IProjectOperationDetailRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<DataResult<List<AssignablePODModel>>?>> Handle(
        GetAssignablePODsQuery request,
        CT ct)
    {
        var result =
            await _repository.GetsAssignableOperationBasedDetailsByProjectOperationId(
                request.ProjectOperationId,
                request.PageIndex,
                request.PageSize,
                ct);

        return new DataResult<List<AssignablePODModel>>
        {
            Data = result.Data,
            RowCount = result.RowCount
        };
    }
}