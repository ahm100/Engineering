using Engineering.Application.Abstractions.Data.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Queries.GetLastRequestMachineryStatusStatement;

public class GetLastRequestMachineryStatusStatementQueryHandler : IQueryHandler<GetLastRequestMachineryStatusStatementQuery, RequestMachineryStatusStatement>
{
    private readonly ILogger<GetLastRequestMachineryStatusStatementQueryHandler> _logger;
    private readonly IRequestMachineryStatusStatementRepository _repository;

    public GetLastRequestMachineryStatusStatementQueryHandler(ILogger<GetLastRequestMachineryStatusStatementQueryHandler> logger,
                                                                IRequestMachineryStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryStatusStatement?>> Handle(GetLastRequestMachineryStatusStatementQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetLastRequestMachineryStatusStatement(
                request.ContractorId, request.CompanyId, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryStatusStatement>(SharedErrors.UnknownError);
        }
    }
}
