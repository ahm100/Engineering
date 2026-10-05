using Engineering.Application.Abstractions.Data.Projects;
using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Commands.UpdateProjectServiceDoneVolume;

public class UpdateProjectServiceDoneVolumeCommandHandler : ICommandHandler<UpdateProjectServiceDoneVolumeCommand, ProjectService>
{
    private readonly ILogger<UpdateProjectServiceDoneVolumeCommand> _logger;
    private readonly IProjectServiceRepository _repository;

    public UpdateProjectServiceDoneVolumeCommandHandler(ILogger<UpdateProjectServiceDoneVolumeCommand> logger, IProjectServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectService?>> Handle(UpdateProjectServiceDoneVolumeCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.UpdateProjectServiceDoneVolume(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectService>(ProjectServiceErrors.ProjectServiceWithIdNotFound);

            var dailyOperationServices = entity.ProjectServiceDetails.SelectMany(x => x.ProjectOperationDetailContractorServices.SelectMany(x => x.DailyOperationServices)).ToList();
            var sumVolume = dailyOperationServices.Where(x => x.ProjectServiceVolume is not null).Sum(x => x.ProjectServiceVolume);
            entity.SetDoneVolume(sumVolume ?? 0);
            entity.SetRemainderVolume();

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