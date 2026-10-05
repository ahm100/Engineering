using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Application.Services.MachineTypes.Models.UpdateMachineType;

namespace Engineering.Application.Services.MachineTypes.Commands.UpdateMachineType;

public class UpdateMachineTypeCommandHandler : ICommandHandler<UpdateMachineTypeCommand, UpdateMachineTypeResponse>
{
    private readonly ILogger<UpdateMachineTypeCommand> _logger;
    private readonly IMachineTypeRepository _repository;
    private readonly ICabinTypeRepository _cabinTypeRepository;

    public UpdateMachineTypeCommandHandler(ILogger<UpdateMachineTypeCommand> logger, IMachineTypeRepository repository, ICabinTypeRepository cabinTypeRepository)
    {
        _logger = logger;
        _repository = repository;
        _cabinTypeRepository = cabinTypeRepository;
    }

    public async Task<Result<UpdateMachineTypeResponse?>> Handle(UpdateMachineTypeCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<UpdateMachineTypeResponse>(MachineTypeErrors.MachineTypeWithIdNotFound);

            var CabinType = await _cabinTypeRepository.GetCabinTypeById(request.CabinTypeId, ct);
            if (CabinType is null)
                return Result.Failure<UpdateMachineTypeResponse>(CabinTypeErrors.CabinTypeWithIdNotFound);

            entity.SetName(request.MachineTypeTitle);
            entity.SetCode(request.MachineTypeCode);
            entity.SetFromWeight(request.FromWeight);
            entity.SetUntilWeight(request.UntilWeight);
            entity.SetCabinType(CabinType);
            entity.SetCompanyId(request.CompanyId);
            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive == true)
                    entity.SetActive();
                else
                    entity.SetInActive();
            }

            await _repository.Update(entity);
            return new UpdateMachineTypeResponse(entity.Id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<UpdateMachineTypeResponse>(SharedErrors.UnknownError);
        }
    }
}