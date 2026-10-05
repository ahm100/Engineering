using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractsDate;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractsDate;

public class GetContractorContractsDateQueryHandler : IQueryHandler<GetContractorContractsDateQuery, GetContractorContractsDateResponse?>
{
    private readonly ILogger<GetContractorContractsDateQueryHandler> _logger;
    private readonly IContractorContractRepository _repository;

    public GetContractorContractsDateQueryHandler(ILogger<GetContractorContractsDateQueryHandler> logger,
                                                 IContractorContractRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetContractorContractsDateResponse?>> Handle(GetContractorContractsDateQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetContractorContractsDate(request.ProjectId, request.ContractorId, request.CompanyId, ct);
            if (result is null || result.Count <= 0)
                return Result.Failure<GetContractorContractsDateResponse>(CSSErrors.NoHaveConfirmedContract);

            var min = result?.Min(x => x.StartDate);
            var max = result?.Max(x => x.EndDate);

            var response = new GetContractorContractsDateResponse();

            response.StartDate = TimeCalculator.DatePiker(min) ?? "";
            response.EndDate = TimeCalculator.DatePiker(max) ?? "";

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetContractorContractsDateResponse?>(SharedErrors.UnknownError);
        }
    }
}
