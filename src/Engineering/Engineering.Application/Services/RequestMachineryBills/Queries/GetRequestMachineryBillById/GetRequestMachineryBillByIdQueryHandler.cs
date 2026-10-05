using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryBills.Queries.GetRequestMachineryBillById;

public class GetRequestMachineryBillByIdQueryHandler : IQueryHandler<GetRequestMachineryBillByIdQuery, RequestMachineryBill>
{
    private readonly ILogger<GetRequestMachineryBillByIdQueryHandler> _logger;
    private readonly IRequestMachineryBillRepository _repository;

    public GetRequestMachineryBillByIdQueryHandler(ILogger<GetRequestMachineryBillByIdQueryHandler> logger, IRequestMachineryBillRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryBill?>> Handle(GetRequestMachineryBillByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetByIdAsync(request.RequestMachineryBillId, ct);
            return result ?? Result.Failure<RequestMachineryBill>(RequestMachineryBillErrors.RequestMachineryBillNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<RequestMachineryBill>(SharedErrors.UnknownError);
        }
    }
}
