using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Application.Services.RequestMachineryBills.Models.GetSupplierDrivers;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryDriver;

public class GetRequestMachineryDriverQueryHandler : IQueryHandler<GetRequestMachineryDriverQuery, GetRequestMachineryDriverModel>
{
    private readonly IRequestMachineryRepository _repository;
    private readonly ILogger<GetRequestMachineryDriverQueryHandler> _logger;

    public GetRequestMachineryDriverQueryHandler(ILogger<GetRequestMachineryDriverQueryHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetRequestMachineryDriverModel?>> Handle(GetRequestMachineryDriverQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetRequestMachineryDriver(request.requestMachineryId, ct);

            return item ?? Result.Failure<GetRequestMachineryDriverModel>(RequestMachineryErrors.FilteredMachineryRequestNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetRequestMachineryDriverModel>(SharedErrors.UnknownError);
        }
    }
}
