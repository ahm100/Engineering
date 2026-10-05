using Engineering.Application.Abstractions.Data.Machineries;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Commands.InactiveMachinery;

public class InactiveMachineryCommandHandler : ICommandHandler<InactiveMachineryCommand, Machinery>
{
    private readonly ILogger<InactiveMachineryCommand> _logger;
    private readonly IMachineryRepository _repository;

    public InactiveMachineryCommandHandler(ILogger<InactiveMachineryCommand> logger, IMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Machinery?>> Handle(InactiveMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
            {
                return Result.Failure<Machinery>(MachineryErrors.MachineryWithIdNotFound);
            }
            if (entity.IsActive == false)
            {
                return Result.Failure<Machinery>(MachineryErrors.IsInactive);
            }
            if (entity.IsDeleted == true)
            {
                return Result.Failure<Machinery>(MachineryErrors.IsDeleted);
            }

            entity.SetDeactivate();

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