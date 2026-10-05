using Engineering.Application.Abstractions.Data.Machineries;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Queries.GetsMachineryForRequestMachinery;

public class GetsMachineryForRequestMachineryQueryHandler : IQueryHandler<GetsMachineryForRequestMachineryQuery, DataResult<List<Machinery>>>
{
    private readonly IMachineryRepository _repository;
    private readonly ILogger<GetsMachineryForRequestMachineryQueryHandler> _logger;

    public GetsMachineryForRequestMachineryQueryHandler(ILogger<GetsMachineryForRequestMachineryQueryHandler> logger, IMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Machinery>>?>> Handle(GetsMachineryForRequestMachineryQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsMachineryForRequestMachinery(request.ProjectId, request.ProjectOperationId, request.ProjectOperationDetailId,
                request.MachineriesGroupId, request.FilterData, request.IsActive, request.CompanyId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<Machinery>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<Machinery>>>(MachineryErrors.FilteredMachineryNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Machinery>>>(SharedErrors.UnknownError);
        }
    }
}