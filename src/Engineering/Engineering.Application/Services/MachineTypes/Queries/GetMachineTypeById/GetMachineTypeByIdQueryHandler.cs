
using Engineering.Application.Abstractions.Data.MachineTypes;
using MachineType = Engineering.Domain.Entities.MachineTypes.MachineType;
namespace Engineering.Application.Services.MachineTypes.Queries.GetMachineTypeById;

public class GetMachineTypeByIdQueryHandler : IQueryHandler<GetMachineTypeByIdQuery, MachineType>
{
    private readonly ILogger<GetMachineTypeByIdQueryHandler> _logger;
    private readonly IMachineTypeRepository _repository;

    public GetMachineTypeByIdQueryHandler(ILogger<GetMachineTypeByIdQueryHandler> logger, IMachineTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineType?>> Handle(GetMachineTypeByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.Id, ct);

            return result ?? Result.Failure<MachineType>(MachineTypeErrors.MachineTypeIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<MachineType>(SharedErrors.UnknownError);
        }
    }
}