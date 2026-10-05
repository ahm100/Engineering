using Engineering.Application.Abstractions.Data.OperationInfos;

namespace Engineering.Application.Services.OperationInfos.Commands.CodeCreator;

public class CodeCreatorCommandHandler : ICommandHandler<CodeCreatorCommand, string?>
{
    private readonly ILogger<CodeCreatorCommand> _logger;
    private readonly IOperationInfoRepository _repository;

    public CodeCreatorCommandHandler(ILogger<CodeCreatorCommand> logger, IOperationInfoRepository repository)
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