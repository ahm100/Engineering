using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements.Enums;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateESSProjectOperationDetailDailyVolume;

public class UpdateESSProjectOperationDetailDailyVolumeCommandHandler : ICommandHandler<UpdateESSProjectOperationDetailDailyVolumeCommand, EmployerStatusStatementProjectOperationDetailDaily>
{
    private readonly ILogger<UpdateESSProjectOperationDetailDailyVolumeCommandHandler> _logger;
    private readonly IEmployerStatusStatementProjectOperationDetailDailyRepository _repository;

    public UpdateESSProjectOperationDetailDailyVolumeCommandHandler(
        ILogger<UpdateESSProjectOperationDetailDailyVolumeCommandHandler> logger,
        IEmployerStatusStatementProjectOperationDetailDailyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<Result<EmployerStatusStatementProjectOperationDetailDaily?>> Handle(UpdateESSProjectOperationDetailDailyVolumeCommand request, CT ct)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        try
        {
            var entity = request.Entity;

            if (entity.EmployerStatusStatementProjectOperationDetail.EmployerStatusStatementProjectOperation.EmployerStatusStatement.Status == EmployerStatusStatementStatus.Pending)
                entity.SetContractorWorkVolume(request.Length, request.Width, request.Height, request.Weight, request.Number, request.Description);

            if (entity.EmployerStatusStatementProjectOperationDetail.EmployerStatusStatementProjectOperation.EmployerStatusStatement.Status == EmployerStatusStatementStatus.SupervisorPending)
                entity.SetSupervisorVolume(request.Length, request.Width, request.Height, request.Weight, request.Number, request.Description);

            if (entity.EmployerStatusStatementProjectOperationDetail.EmployerStatusStatementProjectOperation.EmployerStatusStatement.Status == EmployerStatusStatementStatus.ConsultantPending)
                entity.SetConsultantVolume(request.Length, request.Width, request.Height, request.Weight, request.Number, request.Description);

            if (entity.EmployerStatusStatementProjectOperationDetail.EmployerStatusStatementProjectOperation.EmployerStatusStatement.Status == EmployerStatusStatementStatus.EmployerRepresentativePending)
                entity.SetEmployerRepresentativeVolume(request.Length, request.Width, request.Height, request.Weight, request.Number, request.Description);

            if (request.Urls is not null)
                entity.AddDocuments(request.Urls, false);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerStatusStatementProjectOperationDetailDaily>(SharedErrors.UnknownError);
        }
    }
}
