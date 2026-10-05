using Engineering.Application.Abstractions.Data.Machineries;
using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Queries.HaveMachineriesGroupChild;

public class HaveMachineriesGroupChildQueryHandler : IQueryHandler<HaveMachineriesGroupChildQuery, MachineriesGroup>
{
    private readonly ILogger<HaveMachineriesGroupChildQueryHandler> _logger;
    private readonly IMachineriesGroupRepository _repository;

    public HaveMachineriesGroupChildQueryHandler(ILogger<HaveMachineriesGroupChildQueryHandler> logger, IMachineriesGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineriesGroup?>> Handle(HaveMachineriesGroupChildQuery request, CT ct)
    {
        try
        {
            var result = await _repository.HaveMachineriesGroupChild(request.Id, ct);

            return result ?? Result.Failure<MachineriesGroup>(MachineriesGroupErrors.MachineriesGroupChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<MachineriesGroup>(SharedErrors.UnknownError);
        }
    }
}