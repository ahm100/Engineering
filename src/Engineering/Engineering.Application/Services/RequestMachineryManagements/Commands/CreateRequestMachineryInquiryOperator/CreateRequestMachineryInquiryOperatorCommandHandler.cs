using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.CreateRequestMachineryInquiryOperator;

public class CreateRequestMachineryInquiryOperatorCommandHandler : ICommandHandler<CreateRequestMachineryInquiryOperatorCommand, RequestMachineryInquiryOperator>
{
    private readonly ILogger<CreateRequestMachineryInquiryOperatorCommandHandler> _logger;
    private readonly IRequestMachineryInquiryOperatorRepository _repository;

    public CreateRequestMachineryInquiryOperatorCommandHandler(ILogger<CreateRequestMachineryInquiryOperatorCommandHandler> logger,
                                                               IRequestMachineryInquiryOperatorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryInquiryOperator?>> Handle(CreateRequestMachineryInquiryOperatorCommand request, CT ct)
    {
        try
        {
            var exists = await _repository.GetByMachineryAsync(request.RequestMachinery.Id, request.OperatorAssinmentUserId, ct);
            if (exists is not null)
                return Result.Failure<RequestMachineryInquiryOperator>(RequestMachineryInquiryOperatorErrors.DuplicateRequestMachineryInquiryOperator);

            var entity = new RequestMachineryInquiryOperator(request.OperatorAssinmentId, request.OperatorAssinmentUserId, request.RequestMachinery);

            await _repository.Create(entity, ct);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryInquiryOperator>(SharedErrors.UnknownError);
        }
    }
}
