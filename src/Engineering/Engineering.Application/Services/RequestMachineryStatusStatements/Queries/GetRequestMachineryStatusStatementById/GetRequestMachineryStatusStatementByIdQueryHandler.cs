using Engineering.Application.Abstractions.Data.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Queries.GetRequestMachineryStatusStatementById;

public class GetRequestMachineryStatusStatementByIdQueryHandler : IQueryHandler<GetRequestMachineryStatusStatementByIdQuery, RequestMachineryStatusStatement>
{
    private readonly ILogger<GetRequestMachineryStatusStatementByIdQueryHandler> _logger;
    private readonly IRequestMachineryStatusStatementRepository _repository;

    public GetRequestMachineryStatusStatementByIdQueryHandler(
        ILogger<GetRequestMachineryStatusStatementByIdQueryHandler> logger,
        IRequestMachineryStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryStatusStatement?>> Handle(GetRequestMachineryStatusStatementByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetRequestMachineryStatusStatementById(request.Id, ct);
            if (result is null)
                return Result.Failure<RequestMachineryStatusStatement>(RequestMachineryStatusStatementErrors.RequestMachineryStatusStatementWithIdNotFound);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryStatusStatement>(SharedErrors.UnknownError);
        }
    }
}
