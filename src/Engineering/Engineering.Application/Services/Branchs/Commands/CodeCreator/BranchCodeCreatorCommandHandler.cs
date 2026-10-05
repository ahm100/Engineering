using Engineering.Application.Abstractions.Data.Branchs;

namespace Engineering.Application.Services.Branchs.Commands.CodeCreator;

public class CodeCreatorCommandHandler : ICommandHandler<BranchCodeCreatorCommand, string?>
{
    private readonly ILogger<BranchCodeCreatorCommand> _logger;
    private readonly IBranchRepository _repository;

    public CodeCreatorCommandHandler(
        ILogger<BranchCodeCreatorCommand> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<string?>> Handle(BranchCodeCreatorCommand request, CT ct)
    {
        try
        {
            var result = await _repository.CodeCreator(request.CompanyId, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<string?>(SharedErrors.UnknownError);
        }
    }
}