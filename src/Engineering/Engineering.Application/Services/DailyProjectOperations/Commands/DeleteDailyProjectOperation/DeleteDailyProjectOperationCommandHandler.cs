using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperation;

public class DeleteDailyProjectOperationCommandHandler : ICommandHandler<DeleteDailyProjectOperationCommand, DailyProjectOperation>
{
    private readonly ILogger<DeleteDailyProjectOperationCommandHandler> _logger;
    private readonly IDailyProjectOperationRepository _repository;

    public DeleteDailyProjectOperationCommandHandler(
        ILogger<DeleteDailyProjectOperationCommandHandler> logger,
        IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DailyProjectOperation?>> Handle(DeleteDailyProjectOperationCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<DailyProjectOperation>(ProjectOperationDetailErrors.DataNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<DailyProjectOperation>(ProjectOperationDetailErrors.IsDeletedDaily);
            if (entity.ContractorStatusStatementServices.Any())
            {
                if (entity.ContractorStatusStatementServices.Select(x => x.ContractorStatusStatementDetail.ContractorStatusStatement).ToList().Any())
                    return Result.Failure<DailyProjectOperation>(ProjectOperationDetailErrors.HaveEmployerStatusStatementProjectOperationDetails);
            }
            if (entity.ProjectOperationDetail.EmployerStatusStatementProjectOperationDetails.Any())
            {
                var sumDoneDailies = entity.ProjectOperationDetail.DailyOperations.Sum(x => x.FinalAmount);
                var sumDoneESSPOD = entity.ProjectOperationDetail.EmployerStatusStatementProjectOperationDetails.Sum(x => x.ContractorWorkVolume);
                var sumVolumeESSPOD = entity.ProjectOperationDetail.EmployerStatusStatementProjectOperationDetails.Sum(x => x.TotalWorkVolume);
                if (sumDoneDailies > sumDoneESSPOD)
                {
                    var remaindeDaily = sumDoneDailies - entity.FinalAmount;
                    if (remaindeDaily < sumDoneESSPOD)
                        return Result.Failure<DailyProjectOperation>(ProjectOperationDetailErrors.ESSDoneVolume);
                }
                else
                {
                    if ((sumVolumeESSPOD - sumDoneESSPOD) == 0)
                        return Result.Failure<DailyProjectOperation>(ProjectOperationDetailErrors.ESSDoneVolume);
                }
            }

            entity.SetIsDeleted();

            if (entity.DailyProjectOperationServices.Any())
                foreach (var service in entity.DailyProjectOperationServices)
                {
                    if (service.ContractorStatusStatementServiceDailies.Any())
                        return Result.Failure<DailyProjectOperation>(ProjectOperationDetailErrors.HaveEmployerStatusStatementProjectOperationDetails);
                    service.SetIsDeleted();
                }

            entity.AddHistory();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperation?>(SharedErrors.UnknownError);
        }
    }
}
