using Engineering.Application.Abstractions.Data.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryBills.Queries.IsDuplicateBill;

public class IsDuplicateBillQueryHandler : IQueryHandler<IsDuplicateBillQuery, bool?>
{
    private readonly ILogger<IsDuplicateBillQueryHandler> _logger;
    private readonly IRequestMachineryBillRepository _repository;

    public IsDuplicateBillQueryHandler(ILogger<IsDuplicateBillQueryHandler> logger, IRequestMachineryBillRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(IsDuplicateBillQuery request, CT ct)
    {
        try
        {
            var result = await _repository.IsDuplicateBill(request.RequestMachineryId, request.FromDate, request.ToDate, ct);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}
