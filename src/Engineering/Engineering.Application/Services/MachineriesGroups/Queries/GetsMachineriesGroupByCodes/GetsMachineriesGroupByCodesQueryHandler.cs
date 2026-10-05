using Engineering.Application.Abstractions.Data.Machineries;
using Engineering.Domain.Entities.Machineries;

namespace Engineering.Application.Services.MachineriesGroups.Queries.GetsMachineriesGroupByCodes;

public class GetsMachineriesGroupByCodesQueryHandler : IQueryHandler<GetsMachineriesGroupByCodesQuery, DataResult<List<MachineriesGroup>>>
{
    private readonly IMachineriesGroupRepository _repository;
    private readonly ILogger<GetsMachineriesGroupByCodesQueryHandler> _logger;

    public GetsMachineriesGroupByCodesQueryHandler(ILogger<GetsMachineriesGroupByCodesQueryHandler> logger, IMachineriesGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<MachineriesGroup>>?>> Handle(GetsMachineriesGroupByCodesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsMachineriesGroupByCodes(request.Codes, request.CompanyId, ct);

            return result.Data.Any() ?
                new DataResult<List<MachineriesGroup>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<MachineriesGroup>>>(MachineriesGroupErrors.FilteredMachineriesGroupNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<MachineriesGroup>>>(SharedErrors.UnknownError);
        }
    }
}