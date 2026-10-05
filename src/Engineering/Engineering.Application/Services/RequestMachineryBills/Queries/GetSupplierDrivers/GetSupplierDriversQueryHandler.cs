using Engineering.Application.Abstractions.Data.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryBills.Queries.GetSupplierDrivers;

public class GetSupplierDriversQueryHandler : IQueryHandler<GetSupplierDriversQuery, List<string?>?>
{
    private readonly ILogger<GetSupplierDriversQueryHandler> _logger;
    private readonly IRequestMachineryBillRepository _repository;

    public GetSupplierDriversQueryHandler(ILogger<GetSupplierDriversQueryHandler> logger, IRequestMachineryBillRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<string?>?>> Handle(GetSupplierDriversQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetSupplierDrivers(
                    request.SupplierId,
                    request.FilterData,
                    ct);

            return entities;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<string?>>(SharedErrors.UnknownError);
        }
    }
}
