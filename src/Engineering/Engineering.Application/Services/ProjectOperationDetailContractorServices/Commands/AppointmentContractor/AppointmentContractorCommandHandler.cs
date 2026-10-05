using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetailContractorService = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.AppointmentContractor;

public class AppointmentContractorCommandHandler : ICommandHandler<AppointmentContractorCommand, ProjectOperationDetailContractorService>
{
    private readonly ILogger<AppointmentContractorCommand> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public AppointmentContractorCommandHandler(
        ILogger<AppointmentContractorCommand> logger,
        IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailContractorService?>> Handle(AppointmentContractorCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(
                request.Id,
                ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDetailContractorService>(CostCenterErrors.CostCenterWithIdNotFound);

            entity.SetContractorId(request.ContractorId);

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