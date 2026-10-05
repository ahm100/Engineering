using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Domain.Entities.Projects;
using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Commands.UpdateProjectService;

public class UpdateProjectServiceCommandHandler : ICommandHandler<UpdateProjectServiceCommand, ProjectService>
{
    private readonly ILogger<UpdateProjectServiceCommand> _logger;
    private readonly IProjectServiceRepository _repository;

    public UpdateProjectServiceCommandHandler(ILogger<UpdateProjectServiceCommand> logger, IProjectServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectService?>> Handle(UpdateProjectServiceCommand request, CT ct)
    {
        try
        {
            var entity = request.ProjectService;

            entity.SetServiceInfo(request.ServiceInfo);
            entity.SetContractorId(request.ContractorId);
            entity.SetVolume(request.Volume);
            entity.SetDoneVolume(request.DoneVolume);
            entity.SetRemainderVolume();

            if (request.IsActive == false)
                entity.SetInActive();
            else
                entity.SetActive();

            if (request.OperationInfoServices?.Count > 0)
                foreach (var operationInfoService in request.OperationInfoServices)
                {
                    var newProjectServiceDetail = new ProjectServiceDetail(entity, operationInfoService);
                    entity.AddOperationInfoService(newProjectServiceDetail);
                }

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ProjectService>(SharedErrors.UnknownError);
        }
    }
}