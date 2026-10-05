using Engineering.Application.Abstractions.Data.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.ProjectOperationDetailCodeCreator;

public class CodeCreatorCommandHandler : ICommandHandler<ProjectOperationDetailCodeCreatorCommand, string?>
{
    private readonly ILogger<ProjectOperationDetailCodeCreatorCommand> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public CodeCreatorCommandHandler(ILogger<ProjectOperationDetailCodeCreatorCommand> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<string?>> Handle(ProjectOperationDetailCodeCreatorCommand request, CT ct)
    {
        try
        {
            var result = await _repository.CodeCreator(
                request.ProjectOperationId,
                request.OperationLocationId,
                request.CompanyId, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<string>(SharedErrors.UnknownError);
        }
    }
}
