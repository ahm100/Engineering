using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.Projects.Models.AssignProjectsToCostCenter;

namespace Engineering.Application.Services.Projects.Command.AssignProjectsToCostCenter;

public class AssignProjectsToCostCenterCommandHandler : ICommandHandler<AssignProjectsToCostCenterCommand, AssignProjectsToCostCenterResponse?>
{
    private readonly ILogger<AssignProjectsToCostCenterCommandHandler> _logger;
    private readonly IProjectRepository _repo;
    private readonly ICostCenterRepository _ccRepo;

    public AssignProjectsToCostCenterCommandHandler(
        ILogger<AssignProjectsToCostCenterCommandHandler> logger,
        IProjectRepository repo,
        ICostCenterRepository ccRepo)
    {
        _logger = logger;
        _repo = repo;
        _ccRepo = ccRepo;
    }

    public async Task<Result<AssignProjectsToCostCenterResponse?>> Handle(AssignProjectsToCostCenterCommand request, CT ct)
    {
        try
        {
            var costCenter = await _ccRepo.GetCostCenter(request.CostCenterId, ct);
            if (costCenter is null)
                return Result.Failure<AssignProjectsToCostCenterResponse?>(CostCenterErrors.CostCenterWithIdNotFound);

            var projects = await _repo.GetProjectByIds(request.ProjectIds, false, false, ct);
            if (projects is null || projects.Count < request.ProjectIds.Count)
                return Result.Failure<AssignProjectsToCostCenterResponse?>(ProjectErrors.FilteredProjectNotFound);

            foreach (var item in projects)
            {
                item.SetCostCenter(costCenter);
                await _repo.Update(item);
            }

            return new AssignProjectsToCostCenterResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<AssignProjectsToCostCenterResponse?>(SharedErrors.UnknownError);
        }
    }
}