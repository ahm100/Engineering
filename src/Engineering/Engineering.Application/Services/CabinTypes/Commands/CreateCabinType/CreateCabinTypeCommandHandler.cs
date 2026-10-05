using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Application.Services.CabinTypes.Models.CreateCabinType;
using CabinType = Engineering.Domain.Entities.MachineTypes.CabinType;

namespace Engineering.Application.Services.CabinTypes.Commands.CreateCabinType;

public class CreateCabinTypeCommandHandler : ICommandHandler<CreateCabinTypeCommand, CreateCabinTypeResponse>
{
    private readonly ILogger<CreateCabinTypeCommand> _logger;
    private readonly ICabinTypeRepository _repository;

    public CreateCabinTypeCommandHandler(
        ILogger<CreateCabinTypeCommand> logger,
        ICabinTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CreateCabinTypeResponse?>> Handle(CreateCabinTypeCommand request, CT ct)
    {
        try
        {
            var entity = new CabinType(
                request.CabinTypeName,
                request.CabinTypeCode,
                request.IsActive,
                request.CompanyId);
            var result = await _repository.Create(entity, ct);

            return new CreateCabinTypeResponse
            (
                result.Id,
                result.CabinTypeCode.ToString(),
                result.CabinTypeName
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CreateCabinTypeResponse>(SharedErrors.UnknownError);
        }
    }
}