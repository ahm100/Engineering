
using Engineering.Application.Abstractions.Data.Machineries;
using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Queries.GetMachineriesGroups;

public class GetMachineriesGroupsQueryHandler : IQueryHandler<GetMachineriesGroupsQuery, DataResult<List<MachineriesGroup>>>
{
    private readonly IMachineriesGroupRepository _repository;
    private readonly ILogger<GetMachineriesGroupsQueryHandler> _logger;

    public GetMachineriesGroupsQueryHandler(ILogger<GetMachineriesGroupsQueryHandler> logger, IMachineriesGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<MachineriesGroup>>?>> Handle(GetMachineriesGroupsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetMachineriesGroups(request.Ids, request.FilterData, request.code, request.name, request.isActive, request.CompanyId, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<MachineriesGroup>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<MachineriesGroup>>>(MachineriesGroupErrors.MachineriesGroupChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<MachineriesGroup>>>(SharedErrors.UnknownError);
        }
    }
}