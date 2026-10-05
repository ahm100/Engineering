using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Application.Services.MachineTypes.Queries.GetByCode;

public class GetMachineTypeByCodeQueryHandler : IQueryHandler<GetMachineTypeByCodeQuery, MachineType?>
{
    private readonly ILogger<GetMachineTypeByCodeQuery> _logger;
    private readonly IMachineTypeRepository _repository;

    public GetMachineTypeByCodeQueryHandler(ILogger<GetMachineTypeByCodeQuery> logger, IMachineTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineType?>> Handle(GetMachineTypeByCodeQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.FindByCode(request.MachineTypeCode, request.CompanyId, ct);
            return entity ?? Result.Failure<MachineType>(MachineTypeErrors.TypeCodeIsDuplicate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<MachineType>(SharedErrors.UnknownError);
        }
    }
}