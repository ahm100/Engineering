using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByProductId;

public class GetsProjectOperationDetailByProductIdQueryHandler : IQueryHandler<GetsProjectOperationDetailByProductIdQuery, DataResult<List<ProjectOperationDetail>>>
{
    private readonly IProjectOperationDetailRepository _repository;
    private readonly ILogger<GetsProjectOperationDetailByProductIdQueryHandler> _logger;

    public GetsProjectOperationDetailByProductIdQueryHandler(ILogger<GetsProjectOperationDetailByProductIdQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetail>>?>> Handle(GetsProjectOperationDetailByProductIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByProductId(request.ProjectOperationId, request.ProductId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectOperationDetail>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectOperationDetail>>>(ProjectOperationErrors.ProjectChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperationDetail>>>(SharedErrors.UnknownError);
        }
    }
}