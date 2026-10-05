using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationDependencies.Queries.ValidateProjectOperationDependencyQuery;

public class ValidateProjectOperationDependencyQueryHandler : IQueryHandler<ValidateProjectOperationDependencyQuery, bool?>
{
    private readonly ILogger<ValidateProjectOperationDependencyQueryHandler> _logger;
    private readonly IProjectOperationDependencyRepository _repository;
    private readonly IProjectOperationRepository _pORepo;

    public ValidateProjectOperationDependencyQueryHandler(ILogger<ValidateProjectOperationDependencyQueryHandler> logger,
        IProjectOperationDependencyRepository repository,
        IProjectOperationRepository pORepo)
    {
        _logger = logger;
        _repository = repository;
        _pORepo = pORepo;
    }

    public async Task<Result<bool?>> Handle(ValidateProjectOperationDependencyQuery request, CT ct)
    {
        try
        {
            if (request.ProjectOperationId is not null &&
                request.ActionDate is not null)
            {
                return await ValidateAction(request, ct);
            }

            if (request.PredecessorId is not null &&
                request.SuccessorId is not null &&
                request.Type is not null)
            {
                return await ValidateDependency(request, ct);
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<bool?>> ValidateAction(
    ValidateProjectOperationDependencyQuery request,
    CT ct)
    {
        var dependencies = await _repository.GetByPOId(
            request.ProjectOperationId!.Value,
            ct);

        if (dependencies is null || dependencies.Count < 1)
            return true;

        foreach (var item in dependencies)
        {
            var validationResult = Validate(item, request);

            if (validationResult.IsBad())
                return validationResult;
        }

        return true;
    }

    private async Task<Result<bool?>> ValidateDependency(
    ValidateProjectOperationDependencyQuery request,
    CT ct)
    {
        var predecessor = (
            await _pORepo.GetWithoutIncludeByIds(
                [request.PredecessorId!.Value], ct))
            .FirstOrDefault();

        var successor = (
            await _pORepo.GetWithoutIncludeByIds(
                [request.SuccessorId!.Value], ct))
            .FirstOrDefault();

        if (predecessor is null || successor is null)
            return Result.Failure<bool?>(ProjectOperationErrors.NotFound);

        return ValidateDependency(
            predecessor,
            successor,
            request.Type!.Value);
    }

    private Result<bool?> ValidateDependency(
    ProjectOperation predecessor,
    ProjectOperation successor,
    ProjectOperationDependencyType type)
    {
        switch (type)
        {
            case ProjectOperationDependencyType.FS:
                if (successor.PlannedStartDate < predecessor.PlannedFinishDate)
                    return Result.Failure<bool?>(
                        ProjectOperationDependencyErrors.HaveFSDependency(
                            predecessor.OperationInfo.OperationInfoName));
                break;

            case ProjectOperationDependencyType.SS:
                if (successor.PlannedStartDate < predecessor.PlannedStartDate)
                    return Result.Failure<bool?>(
                        ProjectOperationDependencyErrors.HaveSSDependency(
                            predecessor.OperationInfo.OperationInfoName));
                break;

            case ProjectOperationDependencyType.FF:
                if (successor.PlannedFinishDate < predecessor.PlannedFinishDate)
                    return Result.Failure<bool?>(
                        ProjectOperationDependencyErrors.HaveFFDependency(
                            predecessor.OperationInfo.OperationInfoName));
                break;

            case ProjectOperationDependencyType.SF:
                if (successor.PlannedFinishDate < predecessor.PlannedStartDate)
                    return Result.Failure<bool?>(
                        ProjectOperationDependencyErrors.HaveSFDependency(
                            predecessor.OperationInfo.OperationInfoName));
                break;
        }

        return true;
    }

    private Result<bool?> Validate(
    ProjectOperationDependency item,
    ValidateProjectOperationDependencyQuery request)
    {
        if (item.SuccessorId == request.ProjectOperationId)
        {
            return ValidateAgainstPredecessor(item, request);
        }

        if (item.PredecessorId == request.ProjectOperationId)
        {
            return ValidateAgainstSuccessor(item, request);
        }

        return true;
    }
    private Result<bool?> ValidateAgainstPredecessor(
    ProjectOperationDependency item,
    ValidateProjectOperationDependencyQuery request)
    {
        var name = item.Predecessor.OperationInfo.OperationInfoName;

        return item.DependencyType switch
        {
            ProjectOperationDependencyType.FS =>
                request.ActionDate >= item.Predecessor.PlannedFinishDate
                    ? true
                    : Result.Failure<bool?>(
                        ProjectOperationDependencyErrors.HaveFSDependency(name)),

            ProjectOperationDependencyType.SS =>
                (request.ActionDate >= item.Predecessor.PlannedStartDate &&
                 request.ActionDate <= item.Predecessor.PlannedFinishDate)
                    ? true
                    : Result.Failure<bool?>(
                        ProjectOperationDependencyErrors.HaveSSDependency(name)),

            ProjectOperationDependencyType.FF =>
                request.ActionDate >= item.Predecessor.PlannedFinishDate
                    ? true
                    : Result.Failure<bool?>(
                        ProjectOperationDependencyErrors.HaveFFDependency(name)),

            ProjectOperationDependencyType.SF =>
                (request.ActionDate >= item.Predecessor.PlannedStartDate &&
                 request.ActionDate <= item.Predecessor.PlannedFinishDate)
                    ? true
                    : Result.Failure<bool?>(
                        ProjectOperationDependencyErrors.HaveSFDependency(name)),

            _ => true
        };
    }
    private Result<bool?> ValidateAgainstSuccessor(
    ProjectOperationDependency item,
    ValidateProjectOperationDependencyQuery request)
    {
        var name = item.Successor.OperationInfo.OperationInfoName;

        return item.DependencyType switch
        {
            ProjectOperationDependencyType.FS =>
                request.ActionDate >= item.Successor.PlannedStartDate
                    ? true
                    : Result.Failure<bool?>(
                        ProjectOperationDependencyErrors.HaveFSDependency(name)),

            ProjectOperationDependencyType.SS =>
                (request.ActionDate >= item.Successor.PlannedStartDate &&
                 request.ActionDate <= item.Successor.PlannedFinishDate)
                    ? true
                    : Result.Failure<bool?>(
                        ProjectOperationDependencyErrors.HaveSSDependency(name)),

            ProjectOperationDependencyType.FF =>
                request.ActionDate >= item.Successor.PlannedFinishDate
                    ? true
                    : Result.Failure<bool?>(
                        ProjectOperationDependencyErrors.HaveFFDependency(name)),

            ProjectOperationDependencyType.SF =>
                (request.ActionDate >= item.Successor.PlannedStartDate &&
                 request.ActionDate <= item.Successor.PlannedFinishDate)
                    ? true
                    : Result.Failure<bool?>(
                        ProjectOperationDependencyErrors.HaveSFDependency(name)),

            _ => true
        };
    }
}