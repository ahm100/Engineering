using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Application.Services.MachineTypes.Queries.GetByName;

public class GetMachineTypeByNameQueryHandler : IQueryHandler<GetMachineTypeByNameQuery, MachineType?>
{
    private readonly ILogger<GetMachineTypeByNameQueryHandler> _logger;
    private readonly IMachineTypeRepository _repository;

    public GetMachineTypeByNameQueryHandler(ILogger<GetMachineTypeByNameQueryHandler> logger, IMachineTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineType?>> Handle(GetMachineTypeByNameQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.FindByName(request.MachineTypeTitle, request.CompanyId, ct);

            return entity ?? Result.Failure<MachineType>(MachineTypeErrors.TypeNameIsDuplicate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<MachineType>(SharedErrors.UnknownError);
        }
    }
}