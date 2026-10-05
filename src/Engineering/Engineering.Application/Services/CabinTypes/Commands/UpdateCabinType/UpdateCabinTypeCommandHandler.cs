using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Application.Services.CabinTypes.Models.UpdateCabinType;

namespace Engineering.Application.Services.CabinTypes.Commands.UpdateCabinType;

public class UpdateCabinTypeCommandHandler : ICommandHandler<UpdateCabinTypeCommand, UpdateCabinTypeResponse>
{
    private readonly ILogger<UpdateCabinTypeCommand> _logger;
    private readonly ICabinTypeRepository _repository;

    public UpdateCabinTypeCommandHandler(
        ILogger<UpdateCabinTypeCommand> logger,
        ICabinTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<UpdateCabinTypeResponse?>> Handle(UpdateCabinTypeCommand request, CT ct)
    {
        try
        {
            var cabinTypeEntity = await _repository.GetCabinTypeById(request.Id, ct);
            if (cabinTypeEntity is null)
                return Result.Failure<UpdateCabinTypeResponse>(CabinTypeErrors.CabinTypeWithIdNotFound);

            cabinTypeEntity.SetName(request.CabinTypeName);
            cabinTypeEntity.SetCompanyId(request.CompanyId);

            if (request.IsActive == false)
                cabinTypeEntity.SetInActive();
            else
                cabinTypeEntity.SetActive();

            await _repository.Update(cabinTypeEntity);
            return new UpdateCabinTypeResponse
            (
                cabinTypeEntity.Id,
                cabinTypeEntity.CabinTypeName,
                cabinTypeEntity.CabinTypeCode.ToString()
            );
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<UpdateCabinTypeResponse>(SharedErrors.UnknownError);
        }
    }
}