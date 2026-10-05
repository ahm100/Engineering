using Engineering.Application.Abstractions.Data.Machineries;
using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Queries.GetMachineriesGroupByCode;

public class GetMachineriesGroupByCodeQueryHandler : IQueryHandler<GetMachineriesGroupByCodeQuery, MachineriesGroup>
{
    private readonly ILogger<GetMachineriesGroupByCodeQueryHandler> _logger;
    private readonly IMachineriesGroupRepository _repository;

    public GetMachineriesGroupByCodeQueryHandler(ILogger<GetMachineriesGroupByCodeQueryHandler> logger, IMachineriesGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineriesGroup?>> Handle(GetMachineriesGroupByCodeQuery request, CT ct)
    {
        try
        {
            var MachineriesGroupResponse = await _repository.FindByCode(request.GroupCode, request.CompanyId, ct);

            return MachineriesGroupResponse ?? Result.Failure<MachineriesGroup>(MachineriesGroupErrors.MachineriesGroupWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<MachineriesGroup>(SharedErrors.UnknownError);
        }
    }
}