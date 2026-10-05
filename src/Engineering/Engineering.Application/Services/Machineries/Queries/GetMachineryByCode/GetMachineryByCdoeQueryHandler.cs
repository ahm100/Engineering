using Engineering.Application.Abstractions.Data.Machineries;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Queries.GetMachineryByCode;

public class GetMachineryByCodeQueryHandler : IQueryHandler<GetMachineryByCodeQuery, Machinery?>
{
    private readonly ILogger<GetMachineryByCodeQueryHandler> _logger;
    private readonly IMachineryRepository _repository;

    public GetMachineryByCodeQueryHandler(ILogger<GetMachineryByCodeQueryHandler> logger, IMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Machinery?>> Handle(GetMachineryByCodeQuery request, CT ct)
    {
        try
        {
            var MachineryResponse = await _repository.FindByCode(request.MachineryCode, request.CompanyId, ct);

            return MachineryResponse ?? Result.Failure<Machinery?>(MachineryErrors.MachineryWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Machinery?>(SharedErrors.UnknownError);
        }
    }
}