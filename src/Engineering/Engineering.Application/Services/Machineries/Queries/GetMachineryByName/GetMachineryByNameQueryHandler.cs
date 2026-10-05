using Engineering.Application.Abstractions.Data.Machineries;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Queries.GetMachineryByName;

public class GetMachineryByNameQueryHandler : IQueryHandler<GetMachineryByNameQuery, Machinery?>
{
    private readonly ILogger<GetMachineryByNameQueryHandler> _logger;
    private readonly IMachineryRepository _repository;

    public GetMachineryByNameQueryHandler(ILogger<GetMachineryByNameQueryHandler> logger, IMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Machinery?>> Handle(GetMachineryByNameQuery request, CT ct)
    {
        try
        {
            var MachineryResponse = await _repository.FindByName(request.MachineryName, request.CompanyId, ct);

            return MachineryResponse ?? Result.Failure<Machinery?>(MachineryErrors.MachineryWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Machinery?>(SharedErrors.UnknownError);
        }
    }
}