using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.GetsByProjectOperationDetailId.Queries.GetsByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetInfoByContractorServiceId;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetProjectOperationDetailByIds;

public class GetInfoByContractorServiceIdQueryHandler : IQueryHandler<GetInfoByContractorServiceIdQuery, DataResult<List<GetInfoByContractorServiceIdResponse>>>
{
    private readonly IProjectOperationDetailContractorServiceRepository _repository;
    private readonly ILogger<GetsContractorServiceByProjectOperationDetailIdQueryHandler> _logger;

    public GetInfoByContractorServiceIdQueryHandler(
        ILogger<GetsContractorServiceByProjectOperationDetailIdQueryHandler> logger,
        IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }


    public async Task<Result<DataResult<List<GetInfoByContractorServiceIdResponse>>?>> Handle(GetInfoByContractorServiceIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetInfoByContractorServiceId(request.ContractorServiceIds, ct);

            return result.Any() ?
                new DataResult<List<GetInfoByContractorServiceIdResponse>>
                {
                    Data = result
                } : Result.Failure<DataResult<List<GetInfoByContractorServiceIdResponse>>>(ContractorServiceErrors.NotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetInfoByContractorServiceIdResponse>>>(SharedErrors.UnknownError);
        }
    }


}
