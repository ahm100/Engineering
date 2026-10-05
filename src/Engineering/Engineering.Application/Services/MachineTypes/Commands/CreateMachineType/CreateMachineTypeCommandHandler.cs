using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Application.Services.MachineTypes.Models.CreateMachineType;
using MachineType = Engineering.Domain.Entities.MachineTypes.MachineType;

namespace Engineering.Application.Services.MachineTypes.Commands.CreateMachineType;

public class CreateMachineTypeCommandHandler : ICommandHandler<CreateMachineTypeCommand, CreateMachineTypeResponse>
{
    private readonly ILogger<CreateMachineTypeCommandHandler> _logger;
    private readonly IMachineTypeRepository _repository;
    private readonly ICabinTypeRepository _cabinTypeRepository;

    public CreateMachineTypeCommandHandler(ILogger<CreateMachineTypeCommandHandler> logger, IMachineTypeRepository repository, ICabinTypeRepository cabinTypeRepository)
    {
        _logger = logger;
        _repository = repository;
        _cabinTypeRepository = cabinTypeRepository;
    }

    public async Task<Result<CreateMachineTypeResponse?>> Handle(CreateMachineTypeCommand request, CT ct)
    {
        try
        {
            var CabinType = await _cabinTypeRepository.GetCabinTypeById(
                request.CabinTypeId, ct);
            if (CabinType is null)
                return Result.Failure<CreateMachineTypeResponse>(CabinTypeErrors.CabinTypeWithIdNotFound);

            var newMachineType = new MachineType(request.MachineTypeCode, request.MachineTypeTitle, request.FromWeight, request.UntilWeight, CabinType, request.IsActive, request.CompanyId);
            var result = await _repository.Create(newMachineType, ct);
            return new CreateMachineTypeResponse(result.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CreateMachineTypeResponse>(SharedErrors.UnknownError);
        }
    }
}