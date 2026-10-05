using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.ActiveRequestMachineryInquiryOperator;

public class ActiveRequestMachineryInquiryOperatorCommandHandler : ICommandHandler<ActiveRequestMachineryInquiryOperatorCommand, RequestMachineryInquiryOperator>
{
    private readonly ILogger<ActiveRequestMachineryInquiryOperatorCommandHandler> _logger;
    private readonly IRequestMachineryInquiryOperatorRepository _repository;

    public ActiveRequestMachineryInquiryOperatorCommandHandler(ILogger<ActiveRequestMachineryInquiryOperatorCommandHandler> logger, IRequestMachineryInquiryOperatorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryInquiryOperator?>> Handle(ActiveRequestMachineryInquiryOperatorCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetByMachineryAsync(request.RequestMachineryId, request.OperatorId, ct);
            if (entity is null)
                return Result.Failure<RequestMachineryInquiryOperator>(RequestMachineryInquiryOperatorErrors.RequestMachineryInquiryOperatorNotFound);
            if (entity.IsActive)
                return Result.Failure<RequestMachineryInquiryOperator>(RequestMachineryInquiryOperatorErrors.InValidIsActive);

            entity.SetActive();

            await _repository.Update(entity);

            return entity;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryInquiryOperator>(SharedErrors.UnknownError);
        }
    }
}
