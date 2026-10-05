namespace Engineering.Application.Services.Projects.Commands.CreateProjectCode;

public class CreateProjectCodeCommandHandler : ICommandHandler<CreateProjectCodeCommand, string>
{
    private readonly ILogger<CreateProjectCodeCommand> _logger;

    public CreateProjectCodeCommandHandler(ILogger<CreateProjectCodeCommand> logger)
    {
        _logger = logger;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<Result<string?>> Handle(CreateProjectCodeCommand request, CT ct)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        try
        {
            long PCode = request.ProjectCode;
            var result = "";
            if (string.IsNullOrEmpty(request.CostCenterCode))
                result = $"{request.EmployerSymbol}-{PCode}";
            else
                result = $"{request.CostCenterCode}-{request.EmployerSymbol}-{PCode}";
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<string>(SharedErrors.UnknownError);
        }
    }
}