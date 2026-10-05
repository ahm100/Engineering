using Engineering.Application.Abstractions.Data.Machineries;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Queries.GetMachineryById;

public class GetMachineryByIdQueryHandler : IQueryHandler<GetMachineryByIdQuery, Machinery?>
{
    private readonly ILogger<GetMachineryByIdQueryHandler> _logger;
    private readonly IMachineryRepository _repository;

    public GetMachineryByIdQueryHandler(ILogger<GetMachineryByIdQueryHandler> logger, IMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Machinery?>> Handle(GetMachineryByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindByIdWithMachineriesGroup(request.Id, ct);

            return result ?? Result.Failure<Machinery?>(MachineryErrors.MachineryWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Machinery?>(SharedErrors.UnknownError);
        }
    }
}