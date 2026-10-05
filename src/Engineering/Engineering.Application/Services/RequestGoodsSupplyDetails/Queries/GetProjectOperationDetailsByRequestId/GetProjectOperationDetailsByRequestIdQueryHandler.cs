using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetProjectOperationDetailsByRequestId;

public class GetProjectOperationDetailsByRequestIdQueryHandler : IQueryHandler<GetProjectOperationDetailsByRequestIdQuery, DataResult<List<ProjectOperationDetail>>>
{
    private readonly ILogger<GetProjectOperationDetailsByRequestIdQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetProjectOperationDetailsByRequestIdQueryHandler(ILogger<GetProjectOperationDetailsByRequestIdQueryHandler> logger,
                                                             IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetail>>?>> Handle(GetProjectOperationDetailsByRequestIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationDetailsByRequestIdAsync(request.ProjectOperationId, request.ProductGroupId,
                request.ProjectOperationDetailId, request.FilterData, request.ContractorId, request.PageIndex, request.PageSize, ct);

            var response = result.Data.Any() ? new DataResult<List<ProjectOperationDetail>>
            {
                Data = result.Data,
                RowCount = result.RowCount
            } : Result.Failure<DataResult<List<ProjectOperationDetail>>>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<ProjectOperationDetail>>>(SharedErrors.UnknownError);
        }
    }
}
