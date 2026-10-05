using Engineering.Application.Abstractions.Data.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Queries.GetTodaysPayment;

public class GetTodaysPaymentQueryHandler : IQueryHandler<GetTodaysPaymentQuery, decimal?>
{
    private readonly ILogger<GetTodaysPaymentQueryHandler> _logger;
    private readonly IContractorStatusStatementPaymentRepository _repository;

    public GetTodaysPaymentQueryHandler(ILogger<GetTodaysPaymentQueryHandler> logger, IContractorStatusStatementPaymentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<decimal?>> Handle(GetTodaysPaymentQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetTodaysPayment(ct);

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<decimal?>(SharedErrors.UnknownError);
        }
    }
}
