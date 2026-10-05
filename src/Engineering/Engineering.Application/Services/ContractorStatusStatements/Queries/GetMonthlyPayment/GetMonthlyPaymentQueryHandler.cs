using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetMonthlyPayment;

namespace Engineering.Application.Services.ContractorStatusStatements.Queries.GetMonthlyPayment;

public class GetMonthlyPaymentQueryHandler : IQueryHandler<GetMonthlyPaymentQuery, GetMonthlyPaymentResponse?>
{
    private readonly ILogger<GetMonthlyPaymentQueryHandler> _logger;
    private readonly IContractorStatusStatementPaymentRepository _repository;

    public GetMonthlyPaymentQueryHandler(ILogger<GetMonthlyPaymentQueryHandler> logger, IContractorStatusStatementPaymentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetMonthlyPaymentResponse?>> Handle(GetMonthlyPaymentQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetMonthlyPayment(request.LastMonthsCount, ct);

            return response.Any() ?
                new GetMonthlyPaymentResponse(response, response.Count)
                : Result.Failure<GetMonthlyPaymentResponse?>(SharedErrors.UnknownError);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<GetMonthlyPaymentResponse?>(SharedErrors.UnknownError);
        }
    }
}