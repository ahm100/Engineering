using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetailContractorService = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.UpdateContractorService;

public class UpdateContractorServiceCommandHandler : ICommandHandler<UpdateContractorServiceCommand, ProjectOperationDetailContractorService>
{
    private readonly ILogger<UpdateContractorServiceCommand> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public UpdateContractorServiceCommandHandler(ILogger<UpdateContractorServiceCommand> logger, IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailContractorService?>> Handle(UpdateContractorServiceCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDetailContractorService>(CostCenterErrors.CostCenterWithIdNotFound);


            entity.SetProjectServiceDetail(request.ProjectServiceDetail);
            if (request.ProjectServiceDetail is not null)
            {
                entity.SetServiceInfo(request.ProjectServiceDetail.OperationInfoService);
                entity.SetContractorId(request.ProjectServiceDetail.ProjectService.ContractorId);
            }
            else
            {
                entity.SetServiceInfo(request.OperationInfoService);
                entity.SetContractorId(request.ContractorId);
            }

            entity.SetVolume(request.Volume);
            entity.SetTimeSpant(request.TimeSpant);

            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive == true)
                    entity.SetActive();
                else
                    entity.SetInActive();
            }

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ProjectOperationDetailContractorService>(SharedErrors.UnknownError);
        }
    }
}