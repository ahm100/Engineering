using Engineering.Application.Abstractions.Data.Machineries;
using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Queries.GetsMachineryGroupsForRequestMachinery;

public class GetsMachineryGroupsForRequestMachineryQueryHandler : IQueryHandler<GetsMachineryGroupsForRequestMachineryQuery, DataResult<List<MachineriesGroup>>>
{
    private readonly IMachineriesGroupRepository _repository;
    private readonly ILogger<GetsMachineryGroupsForRequestMachineryQuery> _logger;

    public GetsMachineryGroupsForRequestMachineryQueryHandler(ILogger<GetsMachineryGroupsForRequestMachineryQuery> logger, IMachineriesGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<MachineriesGroup>>?>> Handle(GetsMachineryGroupsForRequestMachineryQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsMachineryGroupsForRequestMachinery(request.ProjectId, request.ProjectOperationIds, request.ProjectOperationDetailIds,
                request.FilterData, request.IsActive, request.CompanyId, request.PageIndex, request.PageSize, ct);

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