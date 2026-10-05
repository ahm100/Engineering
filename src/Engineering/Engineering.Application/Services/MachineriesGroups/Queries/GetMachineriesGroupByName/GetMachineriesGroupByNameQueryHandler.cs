using Engineering.Application.Abstractions.Data.Machineries;
using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Queries.GetMachineriesGroupByName;

public class GetMachineriesGroupByNameQueryHandler : IQueryHandler<GetMachineriesGroupByNameQuery, MachineriesGroup?>
{
    private readonly ILogger<GetMachineriesGroupByNameQueryHandler> _logger;
    private readonly IMachineriesGroupRepository _repository;

    public GetMachineriesGroupByNameQueryHandler(ILogger<GetMachineriesGroupByNameQueryHandler> logger, IMachineriesGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineriesGroup?>> Handle(GetMachineriesGroupByNameQuery request, CT ct)
    {
        try
        {
            var MachineriesGroupResponse = await _repository.FindByName(request.GroupName, request.CompanyId, ct);

            return MachineriesGroupResponse ?? Result.Failure<MachineriesGroup>(MachineriesGroupErrors.MachineriesGroupWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<MachineriesGroup>(SharedErrors.UnknownError);
        }
    }
}