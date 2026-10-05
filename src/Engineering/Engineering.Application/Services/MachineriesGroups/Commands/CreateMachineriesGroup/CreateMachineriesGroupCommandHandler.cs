using Engineering.Application.Abstractions.Data.Machineries;
using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Commands.CreateMachineriesGroup;

public class CreateMachineriesGroupCommandHandler : ICommandHandler<CreateMachineriesGroupCommand, MachineriesGroup?>
{
    private readonly ILogger<CreateMachineriesGroupCommand> _logger;
    private readonly IMachineriesGroupRepository _repository;

    public CreateMachineriesGroupCommandHandler(ILogger<CreateMachineriesGroupCommand> logger, IMachineriesGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineriesGroup?>> Handle(CreateMachineriesGroupCommand request, CT ct)
    {
        try
        {
            var entity = new MachineriesGroup(request.GroupName,
                request.GroupCode,
                request.IsActive,
                request.CompanyId);
            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<MachineriesGroup?>(SharedErrors.UnknownError);
        }
    }
}