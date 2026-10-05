using Engineering.Application.Abstractions.Data.ProcesVerbals;
using Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbals;

namespace Engineering.Application.Services.ProcesVerbal.Queries.GetProcesVerbals;

public class GetProcesVerbalsQueryHandler : IQueryHandler<GetProcesVerbalsQuery, GetProcesVerbalsResponse?>
{
    private readonly ILogger<GetProcesVerbalsQueryHandler> _logger;
    private readonly IProcesVerbalsRepository _repository;

    public GetProcesVerbalsQueryHandler(
        ILogger<GetProcesVerbalsQueryHandler> logger,
        IProcesVerbalsRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetProcesVerbalsResponse?>> Handle(
        GetProcesVerbalsQuery request, CT ct)
    {
        try
        {
            var (data, rowCount) = await _repository.GetProcesVerbals(
                request.TargetId,
                request.ProjectId,
                request.Title,
                request.ContractId,
                request.Type,
                request.PageIndex,
                request.PageSize,
                ct);

            return new GetProcesVerbalsResponse(data, rowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error GetProcesVerbals Query Handler");
            return Result.Failure<GetProcesVerbalsResponse>(SharedErrors.UnknownError)!;
        }
    }
}