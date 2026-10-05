using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsByProjectOperationDetailId;

namespace Engineering.Application.Services.GetsByProjectOperationDetailId.Queries.GetsByProjectOperationDetailId;

public class GetsContractorServiceByProjectOperationDetailIdQueryHandler : IQueryHandler<GetsContractorServiceByProjectOperationDetailIdQuery, DataResult<List<GetsContractorServiceByProjectOperationDetailIdModel>>>
{
    private readonly IProjectOperationDetailContractorServiceRepository _repository;
    private readonly ILogger<GetsContractorServiceByProjectOperationDetailIdQueryHandler> _logger;

    public GetsContractorServiceByProjectOperationDetailIdQueryHandler(
        ILogger<GetsContractorServiceByProjectOperationDetailIdQueryHandler> logger,
        IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsContractorServiceByProjectOperationDetailIdModel>>?>> Handle(GetsContractorServiceByProjectOperationDetailIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsContractorServiceByProjectOperationDetailId(
                request.ProjectOperationDetailId,
                request.ServiceInfoName,
                request.ServiceInfoCode,
                request.OrderBy,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetsContractorServiceByProjectOperationDetailIdModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsContractorServiceByProjectOperationDetailIdModel>>>(ContractorServiceErrors.NotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsContractorServiceByProjectOperationDetailIdModel>>>(SharedErrors.UnknownError);
        }
    }
}