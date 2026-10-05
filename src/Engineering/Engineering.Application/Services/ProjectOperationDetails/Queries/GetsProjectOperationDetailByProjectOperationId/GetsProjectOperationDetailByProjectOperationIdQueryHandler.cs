using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByProjectOperationId;

public class GetsProjectOperationDetailByProjectOperationIdQueryHandler : IQueryHandler<GetsProjectOperationDetailByProjectOperationIdQuery, DataResult<List<ProjectOperationDetailsModel>>>
{
    private readonly IProjectOperationDetailRepository _repository;
    private readonly ILogger<GetsProjectOperationDetailByProjectOperationIdQueryHandler> _logger;

    public GetsProjectOperationDetailByProjectOperationIdQueryHandler(ILogger<GetsProjectOperationDetailByProjectOperationIdQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetailsModel>>?>> Handle(GetsProjectOperationDetailByProjectOperationIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByProjectOperationId(request.Ids, request.ProjectOperationId, request.PrivateName, request.PrivateCode, request.FilterData, request.EmployerId,
                request.Status, request.ContractorIds, request.CreateDate, request.StartDate, request.EndDate, request.ServiceInfoIds, request.ImplementationAssistantIds, request.TechnicalAssistantIds,
                request.CreatorId, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectOperationDetailsModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectOperationDetailsModel>>>(ProjectOperationErrors.ProjectChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperationDetailsModel>>>(SharedErrors.UnknownError);
        }
    }
}