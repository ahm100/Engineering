using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.Projects.Models.GetProjectProgress;

namespace Engineering.Application.Services.Projects.Queries.GetProjectProgress;

public class GetProjectProgressQueryHandler : IQueryHandler<GetProjectProgressQuery, GetProjectProgressResponse?>
{
    private readonly ILogger<GetProjectProgressQueryHandler> _logger;
    private readonly IProjectOperationRepository _pORepo;

    public GetProjectProgressQueryHandler(
        ILogger<GetProjectProgressQueryHandler> logger,
        IProjectOperationRepository pORepo)
    {
        _logger = logger;
        _pORepo = pORepo;
    }

    public async Task<Result<GetProjectProgressResponse?>> Handle(GetProjectProgressQuery request, CT ct)
    {
        try
        {
            var (data, count) = await _pORepo.GetProjectProgress(request.Id, ct);

            var response = new GetProjectProgressResponse(
                data ?? new List<GetProjectProgressModel>(),
                count);

            return Result.Success<GetProjectProgressResponse?>(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return Result.Failure<GetProjectProgressResponse?>(
                SharedErrors.UnknownError);
        }
    }
}