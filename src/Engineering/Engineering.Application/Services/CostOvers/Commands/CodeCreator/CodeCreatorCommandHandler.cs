using Engineering.Application.Abstractions.Data.CostOvers;

namespace Engineering.Application.Services.CostOvers.Commands.CodeCreator;

public class CodeCreatorCommandHandler : ICommandHandler<CodeCreatorCommand, string?>
{
    private readonly ILogger<CodeCreatorCommand> _logger;
    private readonly ICostOverRepository _repository;

    public CodeCreatorCommandHandler(
        ILogger<CodeCreatorCommand> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<string?>> Handle(CodeCreatorCommand request, CT ct)
    {
        try
        {
            var result = await _repository.CodeCreator(request.CompanyId, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<string>(SharedErrors.UnknownError);
        }
    }
}