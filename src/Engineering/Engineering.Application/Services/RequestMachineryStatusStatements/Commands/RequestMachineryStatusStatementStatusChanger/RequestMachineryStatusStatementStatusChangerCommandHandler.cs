using Engineering.Application.Abstractions.Data.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Commands.RequestMachineryStatusStatementStatusChanger;

public class RequestMachineryStatusStatementStatusChangerCommandHandler : ICommandHandler<RequestMachineryStatusStatementStatusChangerCommand, RequestMachineryStatusStatement>
{
    private readonly ILogger<RequestMachineryStatusStatementStatusChangerCommandHandler> _logger;
    private readonly IRequestMachineryStatusStatementRepository _repository;

    public RequestMachineryStatusStatementStatusChangerCommandHandler(ILogger<RequestMachineryStatusStatementStatusChangerCommandHandler> logger, IRequestMachineryStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryStatusStatement?>> Handle(RequestMachineryStatusStatementStatusChangerCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;
            if (request.Status == RequestMachineryStatusStatementStatus.Confirmed)
            {
                if (entity.Status != RequestMachineryStatusStatementStatus.New)
                    return Result.Failure<RequestMachineryStatusStatement>(RequestMachineryStatusStatementErrors.InValidStatus);
            }
            else if (request.Status == RequestMachineryStatusStatementStatus.Rejected)
            {
                if (entity.Status != RequestMachineryStatusStatementStatus.New)
                    return Result.Failure<RequestMachineryStatusStatement>(RequestMachineryStatusStatementErrors.InValidStatus);
            }
            else if (request.Status == RequestMachineryStatusStatementStatus.PaymentConfirmation)
            {
                if (entity.Status != RequestMachineryStatusStatementStatus.Confirmed)
                    return Result.Failure<RequestMachineryStatusStatement>(RequestMachineryStatusStatementErrors.InValidStatus);
            }
            else if (request.Status == RequestMachineryStatusStatementStatus.Paid)
            {
                if (entity.Status != RequestMachineryStatusStatementStatus.PaymentConfirmation)
                    return Result.Failure<RequestMachineryStatusStatement>(RequestMachineryStatusStatementErrors.InValidStatus);
            }

            entity.ChangeStatus(request.Status);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryStatusStatement>(SharedErrors.UnknownError);
        }
    }
}
