using Engineering.Application.Abstractions.Data.Machineries;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Commands.ActiveMachinery;

public class ActiveMachineryCommandHandler : ICommandHandler<ActiveMachineryCommand, Machinery>
{
    private readonly ILogger<ActiveMachineryCommand> _logger;
    private readonly IMachineryRepository _repository;

    public ActiveMachineryCommandHandler(ILogger<ActiveMachineryCommand> logger, IMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Machinery?>> Handle(ActiveMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
            {
                return Result.Failure<Machinery>(MachineryErrors.MachineryWithIdNotFound);
            }
            if (entity.IsActive == true)
            {
                return Result.Failure<Machinery>(MachineryErrors.IsActive);
            }
            if (entity.IsDeleted == true)
            {
                return Result.Failure<Machinery>(MachineryErrors.IsDeleted);
            }

            entity.SetActive();

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Machinery>(SharedErrors.UnknownError);
        }
    }
}