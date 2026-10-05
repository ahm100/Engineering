using Engineering.Application.Abstractions.Data.OperationLocations;

namespace Engineering.Application.Services.OperationLocations.Commands.OperationLocationCodeCreator;

public class OperationLocationCodeCreatorCommandHandler : ICommandHandler<OperationLocationCodeCreatorCommand, string?>
{
    private readonly ILogger<OperationLocationCodeCreatorCommand> _logger;
    private readonly IOperationLocationRepository _repository;

    public OperationLocationCodeCreatorCommandHandler(ILogger<OperationLocationCodeCreatorCommand> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<string?>> Handle(OperationLocationCodeCreatorCommand request, CT ct)
    {
        try
        {
            var result = await _repository.CodeCreator(request.CostCenterId, request.ProjectId, request.ParentId, request.CompanyId, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<string?>(SharedErrors.UnknownError);
        }
    }
}