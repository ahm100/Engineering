using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperationDependencies.Queries.ValidateProjectOperationDependencyQuery;
using Engineering.Application.Services.ProjectOperationDependencies.Queries.WouldCreateCycle;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperationDependencies.Commands.UpdateProjectOperationDependency;

public class UpdateProjectOperationDependencyCommandHandler : ICommandHandler<UpdateProjectOperationDependencyCommand, ProjectOperationDependency>
{
    private readonly ILogger<UpdateProjectOperationDependencyCommand> _logger;
    private readonly IProjectOperationDependencyRepository _repository;
    private readonly IProjectOperationRepository _pOrepository;
    private readonly IMediator _mediator;

    public UpdateProjectOperationDependencyCommandHandler(ILogger<UpdateProjectOperationDependencyCommand> logger,
        IProjectOperationDependencyRepository repository,
        IProjectOperationRepository pOrepository,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _pOrepository = pOrepository;
        _mediator = mediator;
    }

    public async Task<Result<ProjectOperationDependency?>> Handle(UpdateProjectOperationDependencyCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDependency>(ProjectOperationDependencyErrors.DependencyWithIdNotFound);

            ProjectOperation? predecessor = null;
            if (request.PredecessorProjectOperationId is not null)
                predecessor = await _pOrepository.GetProjectOperationById(request.PredecessorProjectOperationId.Value, ct);

            ProjectOperation? successor = null;
            if (request.SuccessorProjectOperationId is not null)
                successor = await _pOrepository.GetProjectOperationById(request.SuccessorProjectOperationId.Value, ct);

            var cycle = await _mediator.Send(new WouldCreateCycleQuery(successor is not null ? successor.Id : entity.SuccessorId, predecessor is not null ? predecessor.Id : entity.PredecessorId), ct);
            if (cycle.Value == true)
                return Result.Failure<ProjectOperationDependency?>(ProjectOperationErrors.CreatesCycle);

            var checkDependency = await _mediator.Send(new ValidateProjectOperationDependencyQuery(null,
                null,
                request.PredecessorProjectOperationId ?? entity.PredecessorId,
                request.SuccessorProjectOperationId ?? entity.SuccessorId,
                request.DependencyType ?? entity.DependencyType), ct);
            if (checkDependency.IsBad())
                return checkDependency.Failure<ProjectOperationDependency?>();

            entity.Update(predecessor,
                successor,
                request.LagDays,
                request.DependencyType);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ProjectOperationDependency>(SharedErrors.UnknownError);
        }
    }
}