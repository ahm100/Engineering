using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements.Enums;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateESSProjectOperationsZeroVolume;

public class UpdateESSProjectOperationsZeroVolumeCommandHandler : ICommandHandler<UpdateESSProjectOperationsZeroVolumeCommand, EmployerStatusStatementProjectOperation>
{
    private readonly ILogger<UpdateESSProjectOperationsZeroVolumeCommandHandler> _logger;
    private readonly IEmployerStatusStatementProjectOperationRepository _repository;

    public UpdateESSProjectOperationsZeroVolumeCommandHandler(
        ILogger<UpdateESSProjectOperationsZeroVolumeCommandHandler> logger,
        IEmployerStatusStatementProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<Result<EmployerStatusStatementProjectOperation?>> Handle(UpdateESSProjectOperationsZeroVolumeCommand request, CT ct)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        try
        {
            var entity = request.Entity;

            if (entity.EmployerStatusStatement.Status == EmployerStatusStatementStatus.Pending)
                entity.SetContractorWorkVolumeToZero(request.Description);

            if (entity.EmployerStatusStatement.Status == EmployerStatusStatementStatus.SupervisorPending)
                entity.SetSupervisorWorkVolumeToZero(request.Description);

            if (entity.EmployerStatusStatement.Status == EmployerStatusStatementStatus.ConsultantPending)
                entity.SetConsultantWorkVolumeToZero(request.Description);

            if (entity.EmployerStatusStatement.Status == EmployerStatusStatementStatus.EmployerRepresentativePending)
                entity.SetEmployerRepresentativeWorkVolumeToZero(request.Description);

            if (request.Urls is not null)
                entity.AddDocuments(request.Urls, false);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerStatusStatementProjectOperation>(SharedErrors.UnknownError);
        }
    }
}
