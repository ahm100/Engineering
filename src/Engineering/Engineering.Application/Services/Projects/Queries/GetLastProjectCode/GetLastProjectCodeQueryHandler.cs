using Engineering.Application.Abstractions.Data.Projects;

namespace Engineering.Application.Services.Projects.Queries.GetLastProjectCode;

public class GetLastProjectCodeQueryHandler : IQueryHandler<GetLastProjectCodeQuery, long>
{
    private readonly ILogger<GetLastProjectCodeQueryHandler> _logger;
    private readonly IProjectRepository _repository;

    public GetLastProjectCodeQueryHandler(
        ILogger<GetLastProjectCodeQueryHandler> logger,
        IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<long>> Handle(GetLastProjectCodeQuery request, CT ct)
    {
        try
        {
            var prefix = ProjectCodeFormat.Prefix(request.EmployerCode, request.CostCenterCode);
            var newCode = await _repository.FindLastProject(prefix, ct);
            return newCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<long>(SharedErrors.UnknownError);
        }
    }

    public static class ProjectCodeFormat
    {
        public static string Prefix(string employerCode, string? costCenterCode)
            => string.IsNullOrWhiteSpace(costCenterCode)
                ? $"{employerCode}-"
                : $"{costCenterCode}-{employerCode}-";
    }
}