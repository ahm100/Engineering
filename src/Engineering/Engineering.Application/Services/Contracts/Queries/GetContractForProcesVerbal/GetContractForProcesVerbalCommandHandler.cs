using Engineering.Application.Abstractions.Data.Contracts;
using Engineering.Application.Services.Contracts.Contracts.GetContractForProcesVerbal;

namespace Engineering.Application.Services.Contracts.Queries.GetContractsByProjectIdForProcesVerbal;

public class GetContractForProcesVerbalCommandHandler : IQueryHandler<GetContractForProcesVerbalCommand, List<GetContractForProcesVerbalResponse>?>
{
    private readonly ILogger<GetContractForProcesVerbalCommandHandler> _logger;
    private readonly IContractRepository _repository;

    public GetContractForProcesVerbalCommandHandler(
        ILogger<GetContractForProcesVerbalCommandHandler> logger,
        IContractRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<GetContractForProcesVerbalResponse>?>> Handle(
        GetContractForProcesVerbalCommand request, CT ct)
    {
        try
        {
            var response = await _repository.GetContractForProcesVerbal(request.ProjectId, ct);
            return Result.Success(response)!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error GetContractsByProjectIdForProcesVerbal: {ProjectId}", request.ProjectId);
            return Result.Failure<List<GetContractForProcesVerbalResponse>>(SharedErrors.UnknownError)!;
        }
    }
}
