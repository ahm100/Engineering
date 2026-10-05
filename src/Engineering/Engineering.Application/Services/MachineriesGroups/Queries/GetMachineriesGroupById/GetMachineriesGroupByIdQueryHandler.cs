using Engineering.Application.Abstractions.Data.Machineries;
using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Queries.GetMachineriesGroupById;

public class GetMachineriesGroupByIdQueryHandler : IQueryHandler<GetMachineriesGroupByIdQuery, MachineriesGroup?>
{
    private readonly ILogger<GetMachineriesGroupByIdQueryHandler> _logger;
    private readonly IMachineriesGroupRepository _repository;

    public GetMachineriesGroupByIdQueryHandler(ILogger<GetMachineriesGroupByIdQueryHandler> logger, IMachineriesGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineriesGroup?>> Handle(GetMachineriesGroupByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindById(request.Id, ct);

            return result ?? Result.Failure<MachineriesGroup?>(MachineriesGroupErrors.MachineriesGroupWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<MachineriesGroup?>(SharedErrors.UnknownError);
        }
    }
}