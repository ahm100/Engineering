using Engineering.Application.Abstractions.Data.ServiceInfos;

namespace Engineering.Application.Services.ServiceInfos.Commands.CodeCreator;

public class CodeCreatorCommandHandler : ICommandHandler<CodeCreatorCommand, string?>
{
    private readonly ILogger<CodeCreatorCommand> _logger;
    private readonly IServiceInfoRepository _repository;

    public CodeCreatorCommandHandler(ILogger<CodeCreatorCommand> logger, IServiceInfoRepository repository)
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
