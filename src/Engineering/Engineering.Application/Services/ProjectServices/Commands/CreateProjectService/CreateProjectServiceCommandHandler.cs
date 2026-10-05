using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Domain.Entities.Projects;
using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Commands.CreateProjectService;

public class CreateProjectServiceCommandHandler : ICommandHandler<CreateProjectServiceCommand, ProjectService>
{
    private readonly ILogger<CreateProjectServiceCommand> _logger;
    private readonly IProjectServiceRepository _repository;

    public CreateProjectServiceCommandHandler(ILogger<CreateProjectServiceCommand> logger, IProjectServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectService?>> Handle(CreateProjectServiceCommand request, CT ct)
    {
        try
        {
            var entity = new ProjectService(
                project: request.Project,
                serviceInfo: request.ServiceInfo,
                contractorId: request.ContractorId,
                volume: request.Volume,
                doneVolume: request.DoneVolume,
                isActive: request.IsActive);

            var result = await _repository.Create(entity, ct);

            if (request.OperationInfoServices?.Count > 0)
                foreach (var operationInfoService in request.OperationInfoServices)
                {
                    var newProjectServiceDetail = new ProjectServiceDetail(entity, operationInfoService);
                    entity.AddOperationInfoService(newProjectServiceDetail);
                }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectService>(SharedErrors.UnknownError);
        }
    }
}