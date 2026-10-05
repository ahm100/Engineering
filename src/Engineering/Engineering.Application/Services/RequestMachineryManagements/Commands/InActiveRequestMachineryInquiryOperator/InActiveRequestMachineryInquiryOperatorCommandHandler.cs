using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.InActiveRequestMachineryInquiryOperator;

public class InActiveRequestMachineryInquiryOperatorCommandHandler : ICommandHandler<InActiveRequestMachineryInquiryOperatorCommand, RequestMachineryInquiryOperator>
{
    private readonly ILogger<InActiveRequestMachineryInquiryOperatorCommandHandler> _logger;
    private readonly IRequestMachineryInquiryOperatorRepository _repository;

    public InActiveRequestMachineryInquiryOperatorCommandHandler(ILogger<InActiveRequestMachineryInquiryOperatorCommandHandler> logger, IRequestMachineryInquiryOperatorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryInquiryOperator?>> Handle(InActiveRequestMachineryInquiryOperatorCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetByMachineryAsync(request.RequestMachineryId, request.OperatorId, ct);
            if (entity is null)
                return Result.Failure<RequestMachineryInquiryOperator>(RequestMachineryInquiryOperatorErrors.RequestMachineryInquiryOperatorNotFound);
            if (!entity.IsActive)
                return Result.Failure<RequestMachineryInquiryOperator>(RequestMachineryInquiryOperatorErrors.InValidIsActive);

            entity.SetInActive();

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
