using Engineering.Application.Abstractions.Data.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperationDependencies.Queries.WouldCreateCycle;

public class WouldCreateCycleQueryHandler : IQueryHandler<WouldCreateCycleQuery, bool?>
{
    private readonly ILogger<WouldCreateCycleQueryHandler> _logger;
    private readonly IProjectOperationDependencyRepository _repository;

    public WouldCreateCycleQueryHandler(ILogger<WouldCreateCycleQueryHandler> logger, IProjectOperationDependencyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(WouldCreateCycleQuery request, CT ct)
    {
        try
        {
            var visited = new HashSet<long>();
            var stack = new Stack<long>();

            stack.Push(request.StartId);

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                if (!visited.Add(current))
                    continue;

                if (current == request.TargetId)
                    return true;

                var dependency = await _repository.GetById(current, ct);

                if (dependency is null)
                    continue;

                stack.Push(dependency.Id);
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}