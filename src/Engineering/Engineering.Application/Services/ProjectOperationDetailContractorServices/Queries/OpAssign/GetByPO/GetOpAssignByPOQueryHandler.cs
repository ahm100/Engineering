using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetByPO;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.OpAssign.GetByPO;

public class GetOpAssignByPOQueryHandler
    : IQueryHandler<
        GetOpAssignByPOQuery,
        DataResult<List<OpAssignModel>>>
{
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public GetOpAssignByPOQueryHandler(
        IProjectOperationDetailContractorServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OpAssignModel>>?>> Handle(
        GetOpAssignByPOQuery request,
        CT ct)
    {
        var result =
            await _repository.GetsOperationBasedAssignmentsByProjectOperationId(
                request.ProjectOperationId,
                request.PageIndex,
                request.PageSize,
                ct);

        return new DataResult<List<OpAssignModel>>
        {
            Data = result.Data,
            RowCount = result.RowCount
        };
    }
}