using Engineering.Application.Abstractions.Data.Machineries;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Commands.UpdateMachinery;

public class UpdateMachineryCommandHandler : ICommandHandler<UpdateMachineryCommand, Machinery>
{
    private readonly ILogger<UpdateMachineryCommand> _logger;
    private readonly IMachineryRepository _repository;

    public UpdateMachineryCommandHandler(ILogger<UpdateMachineryCommand> logger, IMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Machinery?>> Handle(UpdateMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<Machinery>(MachineryErrors.MachineryWithIdNotFound);

            entity.SetName(request.MachineryName);
            entity.SetCode(request.MachineryCode);
            entity.SetMachineriesGroup(request.MachineriesGroup);
            entity.SetCompanyId(request.CompanyId);
            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive == true)
                    entity.SetActive();
                else
                    entity.SetDeactivate();
            }

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<Machinery>(SharedErrors.UnknownError);
        }
    }
}