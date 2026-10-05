using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperationDependencies.Queries.ValidateProjectOperationDependencyQuery;
using Engineering.Application.Services.ProjectOperationDependencies.Queries.WouldCreateCycle;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdIncludelessNew;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperationDependencies.Commands.CreateProjectOperationDependency;

public class CreateProjectOperationDependencyCommandHandler : ICommandHandler<CreateProjectOperationDependencyCommand, ProjectOperationDependency>
{
    private readonly ILogger<CreateProjectOperationDependencyCommand> _logger;
    private readonly IProjectOperationDependencyRepository _repository;
    private readonly IMediator _mediator;

    public CreateProjectOperationDependencyCommandHandler(ILogger<CreateProjectOperationDependencyCommand> logger,
        IProjectOperationDependencyRepository repository,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<Result<ProjectOperationDependency?>> Handle(CreateProjectOperationDependencyCommand request, CT ct)
    {
        try
        {
            if (request.SuccessorProjectOperationId == request.PredecessorProjectOperationId)
                return Result.Failure<ProjectOperationDependency?>(ProjectOperationErrors.CanDependOnSelf);
            var pedeccessor = await _mediator.Send(new GetProjectOperationByIdIncludelessNewQuery(request.PredecessorProjectOperationId), ct);
            if (pedeccessor.IsBad())
                return pedeccessor.Failure<ProjectOperationDependency?>();
            var successor = await _mediator.Send(new GetProjectOperationByIdIncludelessNewQuery(request.SuccessorProjectOperationId), ct);
            if (successor.IsBad())
                return successor.Failure<ProjectOperationDependency?>();

            if (successor.Value!.ProjectId != pedeccessor.Value!.ProjectId)
                return Result.Failure<ProjectOperationDependency>(ProjectOperationErrors.DifferentProjects);

            var haveDependency = await _repository.HaveDependency(request.PredecessorProjectOperationId, request.SuccessorProjectOperationId, ct);
            if (haveDependency)
                return Result.Failure<ProjectOperationDependency>(ProjectOperationErrors.HaveDependency);

            var cycle = await _mediator.Send(new WouldCreateCycleQuery(successor.Value.Id, pedeccessor.Value.Id), ct);
            if (cycle.Value == true)
                return Result.Failure<ProjectOperationDependency?>(ProjectOperationErrors.CreatesCycle);

            var checkDependency = await _mediator.Send(new ValidateProjectOperationDependencyQuery(null, null, request.PredecessorProjectOperationId, request.SuccessorProjectOperationId, request.DependencyType), ct);
            if (checkDependency.IsBad())
                return checkDependency.Failure<ProjectOperationDependency?>();

            var entity = new ProjectOperationDependency(pedeccessor.Value!,
                successor.Value!,
                request.LagDays,
                request.DependencyType);

            var result = await _repository.Create(entity, ct);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ProjectOperationDependency>(SharedErrors.UnknownError);
        }
    }
}