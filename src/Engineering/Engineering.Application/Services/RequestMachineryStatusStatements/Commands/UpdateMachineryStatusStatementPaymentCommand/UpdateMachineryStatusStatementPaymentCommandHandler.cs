using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Application.Abstractions.Data.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Commands.UpdateRequestMachineryStatusStatementPaymentOrder;

public class UpdateMachineryStatusStatementPaymentCommandHandler : ICommandHandler<UpdateMachineryStatusStatementPaymentCommand, RequestMachineryStatusStatement>
{
    private readonly ILogger<UpdateMachineryStatusStatementPaymentCommandHandler> _logger;
    private readonly IRequestMachineryStatusStatementRepository _repository;
    private readonly IRequestMachineryRepository _requestRepository;

    public UpdateMachineryStatusStatementPaymentCommandHandler(
        ILogger<UpdateMachineryStatusStatementPaymentCommandHandler> logger,
        IRequestMachineryStatusStatementRepository repository,
        IRequestMachineryRepository requestRepository
        )
    {
        _logger = logger;
        _repository = repository;
        _requestRepository = requestRepository;
    }

    public async Task<Result<RequestMachineryStatusStatement?>> Handle(UpdateMachineryStatusStatementPaymentCommand request, CT ct)
    {
        try
        {
            var entity = request.StatusStatement;
            var requests = request.RequestMachineries;

            foreach (var item in requests)
            {
                item.ChangeStatus(RequestMachineryStatus.PaidPending);
                item.AddHistory("در انتظار پرداخت");

                await _requestRepository.Update(item);
            }

            entity.SetPaymentOrderId(request.PaymentOrderId);
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
